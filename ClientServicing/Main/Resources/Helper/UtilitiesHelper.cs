using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public class UtilitiesHelper
    {
        public string ReadJson(string parentFolderName, string fileNameAndExt)
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

            Console.WriteLine("======================================================================");
            Console.WriteLine("Request:");
            Console.WriteLine($"\tMethod: {restRequest.Method}");
            Console.WriteLine($"\tResource: {restRequest.Resource}");

            // Request Headers
            Console.WriteLine("\tHeaders:");
            foreach (var header in restRequest.Parameters.Where(p => p.Type == ParameterType.HttpHeader))
            {
                Console.WriteLine($"\t\t{header.Name}: {header.Value}");
            }

            // Request Body
            var requestContent = restRequest.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (requestContent != null)
            {
                Console.WriteLine("\tBody:");
                try
                {
                    if (requestContent.Value is string rawBody)
                    {
                        Console.WriteLine($"\t\t{rawBody}");
                    }
                    else
                    {
                        var requestObj = requestContent.Value;
                        var type = requestObj.GetType();
                        foreach (var prop in type.GetProperties())
                        {
                            var value = prop.GetValue(requestObj);
                            Console.WriteLine($"\t\t{prop.Name}: {value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\t\tError reading body: {ex.Message}");
                }
            }

            // Response Logging
            Console.WriteLine("\n======================================================================");
            Console.WriteLine("Response:");

            // Response Headers
            Console.WriteLine("\tHeaders:");
            if (response.Headers != null)
            {
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"\t\t{header.Name}: {header.Value}");
                }
            }

            // Status Code
            Console.WriteLine($"\tStatus Code: {response.StatusCode}");

            // Response Body
            Console.WriteLine("\tBody:");
            Console.WriteLine($"\t\t{response.Content}");

        }
    }
}
