using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public class UtilitiesHelper
    {
        public string GetApiBaseUrl()
        {
            return "https://horizontest.clientele.co.za/horizon.clientservicing/";
        }
        public string ReadTestDataJson(string parentFolderName, string fileNameAndExt)
        {

            string baseDir = TestContext.CurrentContext.TestDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\"));
            string path = Path.Combine(projectRoot, "Test", "TestData", $"{parentFolderName}", $"{fileNameAndExt}");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at path: {path}");
            }

            string jsonString = File.ReadAllText(path);
            return jsonString;
        }
        public void LogRequestAndResponse(RestRequest restRequest, RestResponse response)
        {
            // Response Logging
            Console.WriteLine("======================================================================");
            Console.WriteLine("Request:");
            Console.WriteLine("======================================================================");
            Console.WriteLine($"Method: {restRequest.Method}");
            Console.WriteLine($"Resource: {restRequest.Resource}");
            // Request Headers
            Console.WriteLine("Parameters:");
            foreach (var parameter in restRequest.Parameters.Where(p => p.Type == ParameterType.GetOrPost))
            {
                Console.WriteLine($"{parameter.Name}: {parameter.Value}");
            }
            // Request Body
            var requestContent = restRequest.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (requestContent != null)
            {
                Console.WriteLine("Body:");
                try
                {
                    if (requestContent.Value is string rawBody)
                    {
                        Console.WriteLine($"{rawBody}");
                    }
                    else
                    {
                        var requestObj = requestContent.Value;
                        var type = requestObj.GetType();
                        //Request Body Properties
                        foreach (var prop in type.GetProperties())
                        {
                            var value = prop.GetValue(requestObj);
                            Console.WriteLine($"{prop.Name}: {value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading body: {ex.Message}");
                }
            }

            // Response Logging
            Console.WriteLine("\n======================================================================");
            Console.WriteLine("Response:");
            Console.WriteLine("======================================================================");
            //Response Base URI
            Console.WriteLine($"Response Uri: {response.ResponseUri}");
            // Response Headers
            Console.WriteLine("Headers:");
            if (response.Headers != null)
            {
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"{header.Name}: {header.Value}");
                }
            }
            // Response Status Code
            Console.WriteLine($"Status Code: {response.StatusCode}");
            // Response Body
            Console.WriteLine("Body:");
            Console.WriteLine($"{prettyPrintJson(response.Content)}");
        }
        public void ValidateJsonSchema(string jsonResponse, string schemaJson)
        {
            JSchema schema = JSchema.Parse(schemaJson);
            JObject json = JObject.Parse(jsonResponse);

            IList<string> validationErrors = new List<string>();
            bool isValid = json.IsValid(schema, out validationErrors);

            Assert.That(isValid, Is.True, $"Schema validation failed: {string.Join(", ", validationErrors)}");
        }
        public string prettyPrintJson(string jsonString)
        {
            using JsonDocument doc = JsonDocument.Parse(jsonString);
            JsonElement root = doc.RootElement;
            var prettyOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
            string prettyJson = JsonSerializer.Serialize(root, prettyOptions);
            return prettyJson;
        }
        public bool? ReadBooleanNullable(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null) return null;
            if (element.ValueKind == JsonValueKind.True) return true;
            if (element.ValueKind == JsonValueKind.False) return false;
            if (element.ValueKind == JsonValueKind.Number && element.TryGetInt32(out int num))
                return num != 0;
            if (element.ValueKind == JsonValueKind.String)
            {
                var str = element.GetString();
                if (string.IsNullOrWhiteSpace(str)) return null;
                // Try parsing "true"/"false"
                if (bool.TryParse(str, out bool boolVal)) return boolVal;
                // Handle "1"/"0"
                if (int.TryParse(str, out int intVal)) return intVal != 0;
                // Optional: Handle "Y"/"N"
                if (str.Equals("Y", StringComparison.OrdinalIgnoreCase)) return true;
                if (str.Equals("N", StringComparison.OrdinalIgnoreCase)) return false;
            }

            return null;

        }
        public int? ReadInt32Nullable(JsonElement element)
        {

            if (element.ValueKind == JsonValueKind.Null) return null;

            // 1) Integer token
            if (element.ValueKind == JsonValueKind.Number && element.TryGetInt32(out int i))
                return i;

            // 2) Numeric token but not an integer (e.g., 100.0000)
            if (element.ValueKind == JsonValueKind.Number)
            {
                // Try decimal to preserve precision
                if (element.TryGetDecimal(out decimal dec))
                {
                    if (dec % 1m == 0) // is integral
                    {
                        // Still ensure it fits Int32 range
                        if (dec <= int.MaxValue && dec >= int.MinValue)
                            return (int)dec;
                    }
                    return null; // not integral
                }
                // Fallback: try double (less precise)
                if (element.TryGetDouble(out double dbl))
                {
                    if (Math.Abs(dbl % 1d) < double.Epsilon)
                    {
                        if (dbl <= int.MaxValue && dbl >= int.MinValue)
                            return (int)dbl;
                    }
                    return null; // not integral
                }
            }

            // 3) String token "100.0000" or "100"
            if (element.ValueKind == JsonValueKind.String)
            {
                var s = element.GetString();

                // First, try direct int
                if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int i2))
                    return i2;

                // Then try decimal with decimals allowed
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal dec2))
                {
                    if (dec2 % 1m == 0 && dec2 <= int.MaxValue && dec2 >= int.MinValue)
                        return (int)dec2;
                }
            }

            return null;


        }
        public string? ReadStringNullable(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null) return null;
            if (element.ValueKind == JsonValueKind.String) return element.GetString();
            return element.ToString();
        }
        public DateTime? ReadDateTimeNullable(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null) return null;
            if (element.ValueKind == JsonValueKind.String && DateTime.TryParse(element.GetString(), out DateTime value)) return value;
            if (element.ValueKind == JsonValueKind.String && element.TryGetDateTime(out var dateExact)) return dateExact;
            return null;
        }
    }
}
