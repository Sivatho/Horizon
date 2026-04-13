using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Email;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Email
{
    public class SendInternalEmailsResponseValidationMethods : AbstractValidationMethods, IPolicyBenefitExtendedMemberResponseValidationMethods
    {

        public void ValidateSendInternalEmailsResponseDataIsNotNullOrEmpty(SendInternalEmailsResponse sendInternalEmailsResponse)
        {
            Assert.That(sendInternalEmailsResponse, Is.Not.Null.Or.Empty, "SendInternalEmailsResponse: should not be null or empty");
        }
        public SendInternalEmailsResponse PopulateSendInternalEmailsResponse(RestResponse response)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(response.Content);
            SendInternalEmailsResponse sendInternalEmailsResponse = new SendInternalEmailsResponse();

            var value = jsonDocument.RootElement.GetString();
            if (!string.IsNullOrEmpty(value))
            {
                sendInternalEmailsResponse.responseString = value;
            }

            return sendInternalEmailsResponse;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                    new() {
                        PropertyName = "Value", // Set required property
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
    }
}
