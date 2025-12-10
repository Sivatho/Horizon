using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenQA.Selenium.BiDi.Network;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public class UtilitiesHelper
    {
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
            Console.WriteLine($"{ prettyPrintJson(response.Content) }");
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
    }
}
