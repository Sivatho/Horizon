using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class CanChangeBankAccountIdValidationMethods : AbstractValidationMethods, ICanChangeBankAccountIdValidationMethods
    {
        public void ValidateCanChangeBankAccountRequestDataIsNotNullOrEmpty(BankAccountRequest canChangeBankAccountRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(canChangeBankAccountRequest,                        Is.Not.Null,                    "Validated: CanChangeBankAccountRequest Should Not Be Null");
                Assert.That(canChangeBankAccountRequest.bankAccountList.Count,   Is.Not.LessThanOrEqualTo(0),   "Validated: BankAccountList.Count Should Not BE Less Than Or Equal To Zero");
            });
            DocumentTemplate.DisplayBody("Validated: CanChangeBankAccountRequest: Is Not Null, Type Of Integer Is True, Integer Is Not Less Than 0");
        }

        public void ValidateCanChangeBankAccountResponseDataIsNotNullAndIsTrueOrFalseAndTypeOfString(CanChangeBankAccountResponse canChangeBankAccountResponse)
        {
            Assert.Multiple(() => { 
                Assert.That(canChangeBankAccountResponse,                   Is.Not.Null,                    "Validated: CanChangeBankAccountResponse Should Not Be Null");
                Assert.That(canChangeBankAccountResponse.data,              Is.Not.Null,                    "Validated: CanChangeBankAccountResponse.Data Should Not Be Null");
                Assert.That(canChangeBankAccountResponse.succeeded,         Is.True.Or.False,               "Validated: canChangeBankAccountResponse.succeeded Should Be Or False");
                Assert.That(canChangeBankAccountResponse.message,           Is.Null.Or.TypeOf<string>(),    "Validated: canChangeBankAccountResponse.message Should Be Null Or Type Of String");
                Assert.That(canChangeBankAccountResponse.errors,            Is.Null.Or.TypeOf<string>(),    "Validated: canChangeBankAccountResponse.errors Should Be Null Or Type Of String");
                Assert.That(canChangeBankAccountResponse.data.proCompleted, Is.True.Or.False,               "Validated: canChangeBankAccountResponse.data.proCompleted Should Be Or False");
                Assert.That(canChangeBankAccountResponse.data.success,      Is.True.Or.False,               "Validated: canChangeBankAccountResponse.data.success Should Be Or False");
                Assert.That(canChangeBankAccountResponse.data.message,      Is.Null.Or.TypeOf<string>(),    "Validated: canChangeBankAccountResponse.data.message Should Be Null Or Type Of String");
            });
            DocumentTemplate.DisplayBody("Validated: CanChangeBankAccountResponse: Is Not Null And Is True Or False And TypeOf String");

        }
        public CanChangeBankAccountResponse PopulateCanChangeBankAccountResponse(RestResponse response)
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(response.Content);

                CanChangeBankAccountResponse canChangeBankAccountResponse = new CanChangeBankAccountResponse
                {
                    data = new CompleteStatusMessages()
                };
                foreach (var property in document.RootElement.EnumerateObject())
                {
                    switch (property.Name)
                    {
                        case "succeeded":
                            canChangeBankAccountResponse.succeeded = property.Value.GetBoolean();
                            break;
                        case "message":
                            canChangeBankAccountResponse.message = property.Value.GetString();
                            break;
                        case "errors":
                            canChangeBankAccountResponse.errors = property.Value.GetString();
                            break;
                        case "data":
                            foreach (var dataProperty in property.Value.EnumerateObject())
                            {
                                switch (dataProperty.Name)
                                {
                                    case "proCompleted":
                                        canChangeBankAccountResponse.data.proCompleted = dataProperty.Value.GetBoolean();
                                        break;
                                    case "success":
                                        canChangeBankAccountResponse.data.success = dataProperty.Value.GetBoolean();
                                        break;
                                    case "message":
                                        canChangeBankAccountResponse.data.message = dataProperty.Value.GetString();
                                        break;
                                }
                            }
                            break;
                        default:
                            TestContext.Out.WriteLine($"Unknown property in response: {property.Name}");
                            break;
                    }
                }
                return canChangeBankAccountResponse;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tCanChangeBankAccount > Exception occurred while deserializing response: {ex.Message}");
                TestContext.Out.WriteLine($"\tCanChangeBankAccount > Stack Trace: {ex.StackTrace}");
                return null;
            }
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
