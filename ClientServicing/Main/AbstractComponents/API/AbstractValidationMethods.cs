using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API
{
    public abstract class AbstractValidationMethods
    {
        abstract public void ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(RestResponse restResponse);
        abstract public void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile);

        public void ValidateHTTPResponseStatusCodeOK(RestResponse restResponse) {
            Assert.That(restResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            TestContext.Out.WriteLine("Response: Status Code is 200 OK");
        }
    }
}
