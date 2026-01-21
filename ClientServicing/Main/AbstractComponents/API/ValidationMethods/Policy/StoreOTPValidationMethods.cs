using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class StoreOTPValidationMethods : AbstractValidationMethods, IStoreOTPValidationMethods
    {
        public StoreOTPResponse populateStoreOTPResponse(RestResponse restResponse)
        {
            UtilitiesHelper utilitiesHelper = new();
            using JsonDocument jsonDoc = JsonDocument.Parse(restResponse.Content);
            var storeOTPResponse = new StoreOTPResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        storeOTPResponse.executionOutcome.succeeded =           (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": storeOTPResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": storeOTPResponse.executionOutcome.errors =   utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data": storeOTPResponse.data =                        (int)utilitiesHelper.ReadInt32Nullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return storeOTPResponse;
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False
                    }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds =  new[] { JsonValueKind.Number }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }

        public void ValidateStoreOTPRequestDataIsNotNullOrEmpty(StoreOTPRequest storeOTPRequest)
        {
            Assert.Multiple(() => {
                Assert.That(storeOTPRequest.policyNo,       Is.GreaterThan(0),      "StoreOTPRequest PolicyNo should be greater than 0");
                Assert.That(storeOTPRequest.managerName,    Is.Not.Null.Or.Empty,   "StoreOTPRequest managerName should not be null or empty");
                Assert.That(storeOTPRequest.managerEmail,   Is.Not.Null.Or.Empty,   "StoreOTPRequest managerEmail should not be null or empty");
                Assert.That(storeOTPRequest.otp,            Is.Not.Null.Or.Empty,   "StoreOTPRequest otp should not be null or empty");
            });
        }

        public void ValidateStoreOTPResponseDataIsNotNullOrEmpty(StoreOTPResponse storeOTPResponse)
        {
            Assert.Multiple(() => {
                Assert.That(storeOTPResponse.executionOutcome,  Is.Not.Null.Or.Not.Empty,   "StoreOTPRequest executionOutcome is Null or Empty.");
                Assert.That(storeOTPResponse.data,              Is.GreaterThanOrEqualTo(0), "StoreOTPRequest  data should be  greater or equal to zero");
            });
        }

    }
}
