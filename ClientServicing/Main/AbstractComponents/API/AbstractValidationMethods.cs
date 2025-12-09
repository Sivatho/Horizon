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
        abstract public void ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(RestResponse restResponse);
        public void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile) {
            UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
            var schemaJson = utilitiesHelper.ReadJson(folder, jsonfile);
            utilitiesHelper.ValidateJsonSchema(restResponse.Content, schemaJson);
            TestContext.Out.WriteLine("ValidateJsonSchema: Response content matches the expected JSON schema and is valid.");
        }

        public void ValidateHTTPResponseStatusCodeOK(RestResponse restResponse) {
            Assert.That(restResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            TestContext.Out.WriteLine("Response: Status Code is 200 OK");
        }
    }
}
