using System.Diagnostics;
using System.Reactive.Concurrency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public static class HttpLoggerHelpers
    {
        private const int MaxLoggedBodyCharsLength = 20000;
       
        private static readonly HashSet<string> SensitiveHeadersNames = new(StringComparer.OrdinalIgnoreCase)
        {
           "Authorization",
            "Proxy-Authorization",
            "X-Api-Key",
            "ApiKey",
            "Ocp-Apim-Subscription-Key",
            "Set-Cookie",
            "Cookie"
        };
        private static readonly HashSet<string> SensitiveBodyFieldNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "password",
            "pass",
            "pwd",
            "secret",
            "token",
            "access_token",
            "refresh_token",
            "client_secret",
            "pin",
            "otp",
            "id",
            "cellphone",
            "legalRefNumber",
            "passportnumber",
            "legalRefNo"
        };
        private static string Redact(string value) => string.IsNullOrEmpty(value) ? value : "***REDACTED***";
        public static void RequestaAndResponseLogging(RestRequest request, RestResponse response, string correlationId = null, Stopwatch? stopwatch = null)
        {
            //Arrange
            stopwatch.Stop();
            TimeSpan? duration = stopwatch.Elapsed;
            correlationId ??= Guid.NewGuid().ToString();
            var durationMs = duration?.TotalMilliseconds;
            var timestamp = $"{DateTimeOffset.UtcNow:O}";
            var durationText = durationMs.HasValue ? $"{durationMs.Value:F0} ms" : "N/A";

            //Act
            DocumentTemplate.DisplayRuler();
            DocumentTemplate.DisplayFieldAndValue("CorrelationId", correlationId);
            DocumentTemplate.DisplayFieldAndValue("Timestamp", timestamp);
            DocumentTemplate.DisplayFieldAndValue("Duration", durationText);
            // Request Details
            DocumentTemplate.DisplayTitle("Request");
            // Request Method and URL
            DocumentTemplate.DisplayFieldAndValue("Method", request.Method.ToString());
            DocumentTemplate.DisplayFieldAndValue("Resource", request.Resource);
            // Request Headers
            DocumentTemplate.DisplaySubTitle("Headers");
            foreach (var header in request.Parameters.Where(p => p.Type == ParameterType.HttpHeader))
            {
                var rawValue = header.Value?.ToString();
                var headerValue = IsSensitiveHeader(header.Name) ? Redact(rawValue) : rawValue;
                DocumentTemplate.DisplayFieldAndValue(header.Name, headerValue);
            }
            // Query/Form Parameters
            var queryParams = request.Parameters.Where(p => p.Type == ParameterType.QueryString).ToList();
            var formParams = request.Parameters.Where(p => p.Type == ParameterType.GetOrPost && p.Name != null).ToList();
            if (queryParams.Any())
            {
                DocumentTemplate.DisplaySubTitle("Query Parameters");
                foreach (var param in queryParams)
                {
                    var rawValue = param.Value?.ToString();
                    var paramValue = IsSensitiveBodyField(param.Name) ? Redact(rawValue) : rawValue;
                    DocumentTemplate.DisplayFieldAndValue(param.Name, paramValue);
                }
            }
            if (formParams.Any()) {
                DocumentTemplate.DisplaySubTitle("Form Parameters");
                foreach (var param in formParams)
                {
                    var rawValue = param.Value?.ToString();
                    var paramValue = IsSensitiveBodyField(param.Name) ? Redact(rawValue) : rawValue;
                    DocumentTemplate.DisplayFieldAndValue(param.Name, paramValue);
                }
            }
            // Request Body (single body parameter in RestSharp)
            var bodyParam = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (bodyParam != null) {
                DocumentTemplate.DisplaySubTitle("Request Body");
                try {
                    string bodyText = ExtractBodyText(bodyParam.Value);
                    bodyText = RedactBodyJsonIfPossible(bodyText);
                   DocumentTemplate.DisplayBody(TruncateIfNeeded(PrettyOrRawJson(bodyText)));
                }
                catch (Exception ex) { 
                    DocumentTemplate.DisplayFieldAndValue("Error extracting request body", ex.Message);
                }
            }

            // Response Details
            DocumentTemplate.DisplayTitle("Response");
            // Response URI & Status
            DocumentTemplate.DisplayFieldAndValue("Response URI", response.ResponseUri?.ToString());
            DocumentTemplate.DisplayFieldAndValue("Status Code", ((int)response.StatusCode).ToString());
            DocumentTemplate.DisplayFieldAndValue("Status Description", response.StatusDescription);
            // Response Headers
            DocumentTemplate.DisplaySubTitle("Headers");
            if(response.Headers != null)
            {
                foreach (var header in response.Headers)
                {
                    var rawValue = header.Value?.ToString();
                    var headerValue = IsSensitiveHeader(header.Name) ? Redact(rawValue) : rawValue;
                    DocumentTemplate.DisplayFieldAndValue(header.Name, headerValue);
                }
            }
            // Error Information
            if (response?.ErrorException != null)
            {
                DocumentTemplate.DisplaySubTitle("Error");
                DocumentTemplate.DisplayFieldAndValue("Error Exception", $"{response.ErrorException.GetType().Name} - {response.ErrorMessage ?? response.ErrorException.Message}");
            }
            else if(!string.IsNullOrWhiteSpace(response?.ErrorMessage))
                DocumentTemplate.DisplayFieldAndValue("Error Message", response.ErrorMessage);
            // Response Body
            DocumentTemplate.DisplaySubTitle("Response Body");
            if (!string.IsNullOrWhiteSpace(response?.Content))
            {
                var content = response.Content;
                content = RedactBodyJsonIfPossible(content);
                DocumentTemplate.DisplayBody(TruncateIfNeeded(PrettyOrRawJson(content)));
            }
            else
            {
                DocumentTemplate.DisplayFieldAndValue("Response Body", "<empty>");
            }
            DocumentTemplate.DisplayRuler();
        }
        private static string ExtractBodyText(object value)
        {
            if (value == null) return string.Empty;
            if (value is string stringValue) return stringValue;
            if (value is byte[] bytesValue) return $"<byte[{bytesValue.Length}] - not logged>";
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return value.ToString();
            }
        }
        private static bool IsSensitiveBodyField(string fieldName) => !string.IsNullOrEmpty(fieldName) && SensitiveBodyFieldNames.Contains(fieldName);
        private static bool IsSensitiveHeader(string headerName) => !string.IsNullOrEmpty(headerName) && SensitiveHeadersNames.Contains(headerName);
        private static string PrettyOrRawJson(string inputValue)
        {
            if (string.IsNullOrWhiteSpace(inputValue)) return string.Empty;
            try
            {
                var parsedJson = JsonConvert.DeserializeObject(inputValue);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch
            {
                return inputValue;
            }
        }
        private static string RedactBodyJsonIfPossible(string bodyText)
        {
            if (string.IsNullOrWhiteSpace(bodyText)) return string.Empty;
            try
            {
                var jsonToken = JToken.Parse(bodyText);
                RedactToken(jsonToken);
                return jsonToken.ToString(Formatting.None);
            }
            catch
            {
                return bodyText;
            }
        }
        private static void RedactToken(JToken? jToken)
        {
            if (jToken is JObject obj)
            {
                foreach (var prop in obj.Properties())
                {
                    if (IsSensitiveBodyField(prop.Name))
                    {
                        prop.Value = $"{prop.Name}: ***REDACTED***";
                    }
                    else
                    {
                        RedactToken(prop.Value);
                    }
                }
            }
            else if (jToken is JArray arr)
            {
                foreach (var item in arr) RedactToken(item);
            }
        }
        private static string TruncateIfNeeded(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            if (text.Length <= MaxLoggedBodyCharsLength) return text;
            return text.Substring(0, MaxLoggedBodyCharsLength) + $"...<truncated {text.Length - MaxLoggedBodyCharsLength} characters>";
        }
    }
}