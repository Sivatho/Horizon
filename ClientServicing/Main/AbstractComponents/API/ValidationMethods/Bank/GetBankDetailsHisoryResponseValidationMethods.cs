using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class GetBankDetailsHisoryResponseValidationMethods : AbstractValidationMethods, IGetBankDetailsHisoryResponseValidationMethods
    {
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] { JsonValueKind.True, JsonValueKind.False }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] { JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] { JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds = new[] { JsonValueKind.Array },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "debitDay",           new[] { JsonValueKind.Number } },
                        { "paymentMethod",      new[] { JsonValueKind.Number } },
                        { "bankAccHolder",      new[] { JsonValueKind.String } },
                        { "bankId",             new[] { JsonValueKind.Number } },
                        { "bankName",           new[] { JsonValueKind.String } },
                        { "bankAccTypeCd",      new[] { JsonValueKind.Number } },
                        { "bankAccTypeDescr",   new[] { JsonValueKind.String } },
                        { "branchCode",         new[] { JsonValueKind.String } },
                        { "bankAccNo",          new[] { JsonValueKind.String } },
                        { "bankAccountId",      new[] { JsonValueKind.Number } },
                        { "entityNo",           new[] { JsonValueKind.Number } },
                        { "effFrom",            new[] { JsonValueKind.String } },
                        { "effTo",              new[] { JsonValueKind.String } },
                        { "audModifyDate",      new[] { JsonValueKind.String } },
                        { "audModifyUser",      new[] { JsonValueKind.String } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Response: All contents and data types are valid as expected.");
        }
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmpty(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getBankDetailHistoryResponse.executionOutcome,  Is.Not.Null.Or.Empty, "GetBankDetailHistory Response: Execution should not be null or empty");
                Assert.That(getBankDetailHistoryResponse.data,              Is.Not.Null.Or.Empty, "GetBankDetailHistory Response:  Data should not be null or empty");
            });
            
            TestContext.Out.WriteLine("Response: ExecutionOutcome and Data are not null or empty as expected.");
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}