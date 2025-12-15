using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class CanChangeBankAccountIdValidationMethods : AbstractValidationMethods, ICanChangeBankAccountIdValidationMethods
    {
        public void ValidateCanChangeBankAccountResponseDataIsNotNullOrEmpty(CanChangeBankAccountResponse canChangeBankAccountResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateResponseIsNotNullOrEmpty(CanChangeBankAccountResponse canChangeBankAccountResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateResponseIsNullOrWhiteSpace(CanChangeBankAccountResponse canChangeBankAccountResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] { JsonValueKind.True, JsonValueKind.False }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds = new[] { JsonValueKind.Object},
                    NestedRules = new Dictionary<string, JsonValueKind[]> {
                        { "proCompleted", new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "success", new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "message", new[] { JsonValueKind.String, JsonValueKind.Null } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Response: content and data types are valid.");
        }
    }
}
