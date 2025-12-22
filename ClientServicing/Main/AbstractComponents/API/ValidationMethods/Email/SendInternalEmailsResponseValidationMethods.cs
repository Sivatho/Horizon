using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Email;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Email
{
    public class SendInternalEmailsResponseValidationMethods : AbstractValidationMethods, ISendInternalEmailsResponseValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(SendInternalEmailsResponse sendInternalEmailsResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule{
                    AllowedKinds = new[]{ 
                        JsonValueKind.String
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.String)
            {
                TestContext.Out.WriteLine("Value: data type is string.");
            }
            else {

                Assert.Fail("Value: data type is not string.");

            }
        }

        public void ValidateSendInternalEmailsResponseDataIsNotNullOrEmpty(SendInternalEmailsResponse sendInternalEmailsResponse)
        {
            Assert.That(sendInternalEmailsResponse, Is.Not.Null.Or.Empty, "SendInternalEmailsResponse: should not be null or empty");
        }
    }
}
