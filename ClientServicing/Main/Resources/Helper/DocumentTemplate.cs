using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public static class DocumentTemplate
    {
        public static void DisplayRuler()
        {
            Console.WriteLine("==========================================================================================================================================\n");
        }
        public static void DisplayTitle(string title)
        {
            Console.WriteLine($"--------------------------- {title} ------------------------------");
        }
        public static void DisplaySubTitle(string subTitle)
        {
            Console.WriteLine($"\n>>> {subTitle} <<<");
        }
        public static void DisplayFieldAndValue(string field, string value)
        {
            Console.WriteLine($"{field}: {value}");
        }
        public static void DisplayBody(string payloadBody)
        {
            Console.WriteLine($"{payloadBody}");
        }
        public static void DisplayResponseEnsureSuccess(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                DocumentTemplate.DisplayTitle("API Call Failed");
                DocumentTemplate.DisplayFieldAndValue("Status Code:", response.StatusCode.ToString());
                DocumentTemplate.DisplayFieldAndValue("Message:", response.Content);
                DocumentTemplate.DisplayRuler();
            }
        }
        public static void DisplayRequestAndResponseExceptionLogging(Exception ex)
        {
            DocumentTemplate.DisplayTitle("Exception Occurred");
            DocumentTemplate.DisplayFieldAndValue("Message:", ex.Message);
            DocumentTemplate.DisplayFieldAndValue("Stack Trace:", ex.StackTrace);
            DocumentTemplate.DisplayRuler();
        }
    }
}
