using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API
{
    public abstract class AbstractValidationMethods
    {
        public void ValidationAssertionHeading() 
        {
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Results:\n======================================================================");
        }
        abstract public void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse);
        public void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile) {
            UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
            var schemaJson = utilitiesHelper.ReadTestDataJson(folder, jsonfile);
            utilitiesHelper.ValidateJsonSchema(restResponse.Content, schemaJson);
            TestContext.Out.WriteLine("Validated: Response JsonSchema content matches the expected JSON schema and is valid.");
        }
        public void ValidateResponseStatusCodeOK(RestResponse restResponse) {
            Assert.That(restResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            TestContext.Out.WriteLine("Validated: Response Status Code is 200 OK");
        }
    }
}
