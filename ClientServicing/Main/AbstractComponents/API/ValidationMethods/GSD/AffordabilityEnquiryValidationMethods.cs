using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.GSD;
using ClientServicing.Main.Models.GSD;
using ClientServicing.Main.Resources.Helper;
using Newtonsoft.Json;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.GSD
{
    public class AffordabilityEnquiryValidationMethods : AbstractValidationMethods, IAffordabilityEnquiryValidationMethods
    {
        public void ValidateAffordabilityEnquiryRequestIsNotNullOrEmpty(AffordabilityEnquiryRequest affordabilityEnquiryRequest)
        {
            throw new NotImplementedException();
        }

        public void ValidateAffordabilityEnquiryResponseIsNotNullOrEmpty(AffordabilityEnquiryResponse affordabilityEnquiryResponse)
        {
            Assert.That(affordabilityEnquiryResponse, Is.Not.Null.Or.Empty, "AffordabilityEnquiryResponse: Should not be null or empty");
            TestContext.Out.WriteLine("Validated: AffordabilityEnquiryResponse is not Null or Empty.");
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> { 
                new JsonValidationRule {
                    PropertyName = "isValid",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errorMessage",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "createdTimestamp",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "requestId",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "identityNumber",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "amount",
                    AllowedKinds = new[] {
                        JsonValueKind.Number, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "initials",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "surname",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errorCodeId",
                    AllowedKinds = new[] {
                        JsonValueKind.Number, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errorCode",
                    AllowedKinds = new[] {
                        JsonValueKind.Number, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "correlationId",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "employeeNumberHash",
                    AllowedKinds = new[] {
                       JsonValueKind.String, JsonValueKind.Null
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }
    }
}
