using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class GetBankDetailsHisoryResponseValidationMethods : AbstractValidationMethods, IGetBankDetailsHisoryResponseValidationMethods
    {
        public override void ValidateResponseContentsIsValid_AndDataTypesIsValid(RestResponse restResponse)
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
                        { "debitDay", new[] { JsonValueKind.Number } },
                        { "paymentMethod", new[] { JsonValueKind.Number } },
                        { "bankAccHolder", new[] { JsonValueKind.String } },
                        { "bankId", new[] { JsonValueKind.Number } },
                        { "bankName", new[] { JsonValueKind.String } },
                        { "bankAccTypeCd", new[] { JsonValueKind.Number } },
                        { "bankAccTypeDescr", new[] { JsonValueKind.String } },
                        { "branchCode", new[] { JsonValueKind.String } },
                        { "bankAccNo", new[] { JsonValueKind.String } },
                        { "bankAccountId", new[] { JsonValueKind.Number } },
                        { "entityNo", new[] { JsonValueKind.Number } },
                        { "effFrom", new[] { JsonValueKind.String } },
                        { "effTo", new[] { JsonValueKind.String } },
                        { "audModifyDate", new[] { JsonValueKind.String } },
                        { "audModifyUser", new[] { JsonValueKind.String } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
        }
        public void ValidateBankDetailHistoryDataIsNotNullOrEmpty(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.That(getBankDetailHistoryResponse.executionOutcome.succeeded, Is.True, "GetBankDetailHistory Response: Succeeded should be null or empty");
            foreach (var bankDetailHistoryData in getBankDetailHistoryResponse.data)
            {
                Assert.That(bankDetailHistoryData.bankAccHolder, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Bank Account Holder should be null or empty");
                Assert.That(bankDetailHistoryData.bankName, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Bank Name should be null or empty");
                Assert.That(bankDetailHistoryData.bankAccTypeDescr, Is.Null.Or.Empty, "GetBankDetailHistory Response: Bank Account Type Description should be null or empty");
                Assert.That(bankDetailHistoryData.branchCode, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Branch Code should be null or empty");
                Assert.That(bankDetailHistoryData.bankAccNo, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Bank Account Number should be null or empty");
                Assert.That(bankDetailHistoryData.effFrom, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Effective From should be null or empty");
                Assert.That(bankDetailHistoryData.effTo, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Effective To should be null or empty");
                Assert.That(bankDetailHistoryData.audModifyDate, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Audit Modify Date should be null or empty");
                Assert.That(bankDetailHistoryData.audModifyUser, Is.Not.Null.And.Not.Empty, "GetBankDetailHistory Response: Audit Modify User should be null or empty");
            }
        }
        public void ValidateResponseIsNotNullOrEmpty(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.That(getBankDetailHistoryResponse.executionOutcome, Is.Not.Null.Or.Empty, "GetBankDetailHistory Response: Execution should not be null or empty");
            Assert.That(getBankDetailHistoryResponse.data, Is.Not.Null.Or.Empty, "GetBankDetailHistory Response:  Data should not be null or empty");
        }
        public void ValidateResponseIsNullOrWhiteSpace(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.That(getBankDetailHistoryResponse.executionOutcome.message, Is.Null.Or.Empty, "GetBankDetailHistory Response: Message should be null or empty");
            Assert.That(getBankDetailHistoryResponse.executionOutcome.errors, Is.Null.Or.Empty, "GetBankDetailHistory Response: Errors should be null or empty");

            foreach (var bankDetailHistoryData in getBankDetailHistoryResponse.data)
            {
                Assert.That(bankDetailHistoryData.bankAccHolder, Is.Null.Or.Empty, "GetBankDetailHistory Response: Bank Account Holder should be null or empty");
                Assert.That(bankDetailHistoryData.bankName, Is.Null.Or.Empty, "GetBankDetailHistory Response: Bank Name should be null or empty");
                Assert.That(bankDetailHistoryData.bankAccTypeDescr, Is.Null.Or.Empty, "GetBankDetailHistory Response: Bank Account Type Description should be null or empty");
                Assert.That(bankDetailHistoryData.branchCode, Is.Null.Or.Empty, "GetBankDetailHistory Response: Branch Code should be null or empty");
                Assert.That(bankDetailHistoryData.bankAccNo, Is.Null.Or.Empty, "GetBankDetailHistory Response: Bank Account Number should be null or empty");
                Assert.That(bankDetailHistoryData.effFrom, Is.Null.Or.Empty, "GetBankDetailHistory Response: Effective From should be null or empty");
                Assert.That(bankDetailHistoryData.effTo, Is.Null.Or.Empty, "GetBankDetailHistory Response: Effective To should be null or empty");
                Assert.That(bankDetailHistoryData.audModifyDate, Is.Null.Or.Empty, "GetBankDetailHistory Response: Audit Modify Date should be null or empty");
                Assert.That(bankDetailHistoryData.audModifyUser, Is.Null.Or.Empty, "GetBankDetailHistory Response: Audit Modify User should be null or empty");
            }
            TestContext.Out.WriteLine("GetBankDetailHistory Response: All fields are null or empty as expected.");
        }
        public override void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile)
        {
            UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
            var schemaJson = utilitiesHelper.ReadJson(folder, jsonfile);
            utilitiesHelper.ValidateJsonSchema(restResponse.Content, schemaJson);
        }
    }
}