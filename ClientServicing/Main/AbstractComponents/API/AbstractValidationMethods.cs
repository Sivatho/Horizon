using System.Net;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Resources.Helper;
using Newtonsoft.Json.Schema;
using RestSharp;
using JsonSchema = ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation.JsonSchema;

namespace ClientServicing.Main.AbstractComponents.API
{
    public abstract class AbstractValidationMethods
    {
        public void ValidationAssertionHeading()
        {
            var text = "API Response Validation Assertions";
            DocumentTemplate.DisplayTitle(text);    
        }
        private string BuildStatusFailureMessage(RestResponse restResponse, HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode)
        {
            const int maxBodyPreviewLength = 500;
            var statusLine = $"Expected: {(int)expectedStatusCode} {expectedStatusCode} | Actual: {(int)actualStatusCode} {actualStatusCode}";
            var uri = restResponse.ResponseUri?.ToString() ?? "Unknown URI";
            var method = restResponse.Request?.Method.ToString() ?? "Unknown Method";
            var contentType = restResponse.ContentType ?? "Unknown Content-Type";
            var contentLength = restResponse.RawBytes?.Length.ToString() ?? (restResponse.Content?.Length.ToString() ?? "Unknown Length");
            var bodyPreview = restResponse.Content;

            if (!string.IsNullOrEmpty(bodyPreview) && bodyPreview.Length > maxBodyPreviewLength)
            {
                bodyPreview = bodyPreview.Substring(0, maxBodyPreviewLength) + "... [truncated]";
            }
            return $@"❌ Status Code Assertion Failed!
      {statusLine}
      Request: {method} {uri}
      Content-Type: {contentType}
      Content-Length: {contentLength}
      Body (preview): {bodyPreview}";
        }

        public void ValidateResponseStatusCode(RestResponse restResponse, HttpStatusCode expectedStatusCode)
        {
            Assert.That(restResponse, Is.Not.Null, "Response Should Not Be Null");
            var actualStatusCode = restResponse.StatusCode;
            Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode), () => (string)BuildStatusFailureMessage(restResponse, expectedStatusCode, actualStatusCode));
            DocumentTemplate.DisplayBody($"Validated: Response Status Code: {(int)actualStatusCode}; Status Description: '{actualStatusCode}' as expected.");            
        }
            var actualErrorMessage = restResponse?.Content ?? string.Empty;

            Assert.That(
                  actualErrorMessage,
                  Is.EqualTo(expectedErrorMessage),
                  "Response error message should match the expected value."
              );


            DocumentTemplate.DisplayBody(
                   $"Validated: Response Error Message: {actualErrorMessage} matches Expected Error Message: {expectedErrorMessage}."
               );
        }
        public void ValidateResponseHeadersAreValid(RestResponse restResponse)
        {
            if (restResponse.Headers.Count == 0)
            {
                Assert.Fail("Response Headers should not be empty");
            }
            else
            {
                restResponse.Headers.ToList().ForEach(header =>
                {
                    Assert.Multiple(() =>
                    {
                        Assert.That(header.Name, Is.Not.Null.Or.Empty, "Response Header Name should not be null or empty");
                        Assert.That(header.Value, Is.Not.Null.Or.Empty, $"Response Header '{header.Name}' value should not be null or empty");
                    });
                    Assert.Multiple(() =>
                    {
                        switch (header.Name)
                        {
                            case "Transfer-Encoding": Assert.That(header.Value, Is.EqualTo("chunked")); break;
                            case "Server": Assert.That(header.Value, Is.EqualTo("Microsoft-IIS/10.0")); break;
                            case "Strict-Transport-Security": Assert.That(header.Value, Is.EqualTo("max-age=2592000")); break;
                            case "api-supported-versions": Assert.That(header.Value, Is.EqualTo("1.0")); break;
                            case "X-Powered-By": Assert.That(header.Value, Is.EqualTo("ASP.NET")); break;
                        }
                    });
                });
                TestContext.Out.WriteLine("Validated: Response Header Is Valid");
            }
        }
        public void ValidateResponseDataShouldAcceptValidNames_And_Types(RestResponse restResponse, JsonSchema jsonSchema)
        {
            var schema = jsonSchema;
            restResponse.Data_Should_Accept_Valid_Names_And_Types(schema);
        }
        public void ValidateResponseShouldMatchSchema(RestResponse restResponse, JsonSchema jsonSchema)
        {
            var schema = jsonSchema;
            restResponse.ShouldMatchSchema(schema);
        }

        public void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile)
        {
            UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
            var schemaJson = utilitiesHelper.ReadTestDataJson(folder, jsonfile);
            utilitiesHelper.ValidateJsonSchema(restResponse.Content, schemaJson);
            TestContext.Out.WriteLine("Validated: Response JsonSchema content matches the expected JSON schema and is valid.");
        }       
        abstract public void ValidateResponseFieldParametersIsValid(RestResponse restResponse);
        abstract public void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse); // To be removed and all classes consuming it 
    }
}