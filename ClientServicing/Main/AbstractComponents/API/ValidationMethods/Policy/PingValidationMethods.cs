using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class PingValidationMethods : AbstractValidationMethods, IPingValidationMethods
    {
        public void ValidatePingResponseDataIsNotNullOrEmpty(PingResponse pingResponse)
        {
            Assert.That(pingResponse, Is.Not.Null.Or.Empty);
            TestContext.Out.WriteLine("Validated: Ping Response data is not null or empty.");
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new() {
                    PropertyName = "YourPropertyName", // Replace with the actual property name to validate
                    AllowedKinds = new[]{
                        JsonValueKind.String
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.True)
            {
                TestContext.Out.WriteLine("Validated: Response data type is true.");
            }
            else
            {
                Assert.Fail("Validated: Response data type is false.");
            }
        }
    }
}
