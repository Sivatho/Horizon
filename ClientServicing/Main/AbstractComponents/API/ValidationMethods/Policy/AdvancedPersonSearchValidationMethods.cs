using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class AdvancedPersonSearchValidationMethods : AbstractValidationMethods, IAdvancedPersonSearchValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public void ValidateAdvancedPersonSearchRequestDataIsNotNullorEmpty(AdvancedPersonSearchRequest advancedPersonSearchRequest)
        {
            throw new NotImplementedException();
        }

        public void ValidateAdvancedPersonSearchResponseDataIsNotNullOrEmpty(AdvancedPersonSearchResponse advancedPersonSearchResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(advancedPersonSearchResponse.executionOutcome, Is.Not.Null.Or.Not.Empty, "Execution Outcome is Null or Empty.");
                Assert.That(advancedPersonSearchResponse.data, Is.Not.Null.Or.Not.Empty, "Execution Outcome is Null or Empty.");
            });
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
                    AllowedKinds =  new[] { JsonValueKind.Array }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }
        public AdvancedPersonSearchResponse populateAdvancedPersonSearchResponse(RestResponse restResponse)
        {

            using JsonDocument jsonDoc = JsonDocument.Parse(restResponse.Content);
            var advancedPersonSearchResponse = new AdvancedPersonSearchResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        advancedPersonSearchResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        advancedPersonSearchResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        advancedPersonSearchResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var dataValue = property.Value;
                        switch (dataValue.ValueKind)
                        {
                            case JsonValueKind.Array:
                                TestContext.Out.WriteLine("Data: data type is array");
                                break;
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return advancedPersonSearchResponse;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
