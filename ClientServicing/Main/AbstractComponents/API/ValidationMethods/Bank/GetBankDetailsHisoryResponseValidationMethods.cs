using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class GetBankDetailsHisoryResponseValidationMethods : AbstractValidationMethods, IGetBankDetailsHisoryResponseValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateGetBankDetailsHisoryResponseRequestIsNotNullOrEmy(PolicyNoRequest getBankDetailsHisoryResponseRequest)
        {
            Assert.That(getBankDetailsHisoryResponseRequest,                    Is.Not.Null,                        "Response: GetBankDetailsHisoryResponseRequest should not be null or empty");
            Assert.That(getBankDetailsHisoryResponseRequest.policyNo,           Is.GreaterThanOrEqualTo(0),     "Response: GetBankDetailsHisoryResponseRequest.PolicyNo should not be null or empty");
            Assert.That(getBankDetailsHisoryResponseRequest.policyNoList.Count, Is.GreaterThanOrEqualTo(0),         "Response: GetBankDetailsHisoryResponseRequest.PolicyNo.Count should be greater than or equal to zero");
        }
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountGreaterThanZero(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getBankDetailHistoryResponse.executionOutcome,  Is.Not.Null.Or.Empty, "Response: GetBankDetailHistoryResponse.Execution should not be null or empty");
                Assert.That(getBankDetailHistoryResponse.data,              Is.Not.Null.Or.Empty, "Response: GetBankDetailHistory.Data hould not be null or empty");

                Assert.That(getBankDetailHistoryResponse.executionOutcome.succeeded,    Is.True.Or.False,               "Response: GetBankDetailHistoryResponse.Execution.Succeeded should be true or false"); 
                Assert.That(getBankDetailHistoryResponse.executionOutcome.message,      Is.Null.Or.TypeOf<string>(),    "Response: GetBankDetailHistoryResponse.Execution.Message should be null or type of string"); 
                Assert.That(getBankDetailHistoryResponse.executionOutcome.errors,       Is.Null.Or.TypeOf<string>(),    "Response: GetBankDetailHistoryResponse.Execution.Errors should be null or type of string");

                Assert.That(getBankDetailHistoryResponse.data.Count,                    Is.GreaterThan(0), "Response: GetBankDetailHistoryResponse .data.Count should be greater than 0");
                foreach (var data in getBankDetailHistoryResponse.data) {
                    Assert.That(data.debitDay,          Is.GreaterThanOrEqualTo(0),         "Response: GetBankDetailHistoryResponse.Data.DebitDay should be greater than or equal to zero");
                    Assert.That(data.paymentMethod,     Is.GreaterThanOrEqualTo(0),         "Response: GetBankDetailHistoryResponse.Data.PaymentMethod should be greater than or equal to zero");
                    Assert.That(data.bankAccHolder,     Is.Not.Null.And.Not.Empty,          "Response: GetBankDetailHistoryResponse.Data.BankAccHolder should not be null or empty");
                    Assert.That(data.bankId,            Is.GreaterThanOrEqualTo(0),         "Response: GetBankDetailHistoryResponse.Data.BankId should be greater than or equal to zero");
                    Assert.That(data.bankName,          Is.Not.Null.And.Not.Empty,          "Response: GetBankDetailHistoryResponse.Data.BankName should not be null or empty");
                    Assert.That(data.bankAccTypeCd,     Is.GreaterThanOrEqualTo(0),         "Response: GetBankDetailHistoryResponse.Data.BankAccTypeCd should be greater than or equal to zero");
                    Assert.That(data.bankAccTypeDescr,  Is.Not.Null.And.Not.Empty,          "Response: GetBankDetailHistoryResponse.Data.BankAccTypeDescr should not be null or empty");
                    Assert.That(data.branchCode,        Is.Not.Null.Or.Empty,               "Response: GetBankDetailHistoryResponse.Data.BranchCode should not be null or empty");
                    Assert.That(data.bankAccNo,         Is.Not.Null.And.Not.Empty,          "Response: GetBankDetailHistoryResponse.Data.BankAccNo should not be null or empty");
                    Assert.That(data.bankAccountId,     Is.GreaterThan(0),                  "Response: GetBankDetailHistoryResponse.Data.BankAccountId should be greater than 0");
                    Assert.That(data.entityNo,          Is.GreaterThan(0),                  "Response: GetBankDetailHistoryResponse.Data.EntityNo should be greater than 0");
                    Assert.That(data.effFrom,           Is.Not.EqualTo(default(DateTime)),  "Response: GetBankDetailHistoryResponse.Data.EffFrom should not be equal to default dateTime");
                    Assert.That(data.effTo,             Is.Not.EqualTo(default(DateTime)),  "Response: GetBankDetailHistoryResponse.Data.EffTo should not be equal to default dateTime");
                    Assert.That(data.audModifyDate,     Is.Not.EqualTo(default(DateTime)),  "Response: GetBankDetailHistoryResponse.Data.AudModifyDate should not be equal to default dateTime");
                    Assert.That(data.audModifyUser,     Is.Not.Null.And.Not.Empty,          "Response: GetBankDetailHistoryResponse.Data.AudModifyUser should not be equal to default dateTime");
                }
            });
            
            TestContext.Out.WriteLine("Response: GetBankDetailHistoryResponse ExecutionOutcome and Data are not null or empty as expected.");
        }
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountLessThanOrEqualToZero(GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getBankDetailHistoryResponse.executionOutcome,              Is.Not.Null.Or.Empty,           "Response: GetBankDetailHistoryResponse.Execution should not be null or empty");
                Assert.That(getBankDetailHistoryResponse.data,                          Is.Not.Null.Or.Empty,           "Response: GetBankDetailHistory.Data hould not be null or empty");

                Assert.That(getBankDetailHistoryResponse.executionOutcome.succeeded,    Is.True.Or.False,               "Response: GetBankDetailHistoryResponse.Execution.Succeeded should be true or false");
                Assert.That(getBankDetailHistoryResponse.executionOutcome.message,      Is.Null.Or.TypeOf<string>(),    "Response: GetBankDetailHistoryResponse.Execution.Message should be null or type of string");
                Assert.That(getBankDetailHistoryResponse.executionOutcome.errors,       Is.Null.Or.TypeOf<string>(),    "Response: GetBankDetailHistoryResponse.Execution.Errors should be null or type of string");

                Assert.That(getBankDetailHistoryResponse.data,                          Is.Not.Null.And.Empty,    "Response: GetBankDetailHistoryResponse .data.Count should be greater than 0");
                Assert.That(getBankDetailHistoryResponse.data.Count,                    Is.LessThanOrEqualTo(0),                 "Response: GetBankDetailHistoryResponse .data.Count should be greater than 0");
            });
            TestContext.Out.WriteLine("Response: GetBankDetailHistoryResponse ExecutionOutcome is not null or empty and Data is Empty as expected.");
        }
        public GetBankDetailHistoryResponse PopulateGetBankDetailHistoryResponse(RestResponse response)
        {
            using JsonDocument doc = JsonDocument.Parse(response.Content!);
            GetBankDetailHistoryResponse getBankDetailHistoryResponse = new GetBankDetailHistoryResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new List<GetBankDetailHistory>()
            };
            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":   getBankDetailHistoryResponse.executionOutcome.succeeded =   property.Value.GetBoolean(); break;
                    case "message":     getBankDetailHistoryResponse.executionOutcome.message =     property.Value.GetString(); break;
                    case "errors":      getBankDetailHistoryResponse.executionOutcome.errors =      property.Value.GetString(); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            GetBankDetailHistory bankDetailHistory = new();
                            foreach (var data in item.EnumerateObject())
                            {
                                switch (data.Name)
                                {
                                    case "debitDay":            bankDetailHistory.debitDay =            (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "paymentMethod":       bankDetailHistory.paymentMethod =       (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "bankAccHolder":       bankDetailHistory.bankAccHolder =       utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    case "bankId":              bankDetailHistory.bankId =              (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "bankName":            bankDetailHistory.bankName =            utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    case "bankAccTypeCd":       bankDetailHistory.bankAccTypeCd =       (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "bankAccTypeDescr":    bankDetailHistory.bankAccTypeDescr =    utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    case "branchCode":          bankDetailHistory.branchCode =          utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    case "bankAccNo":           bankDetailHistory.bankAccNo =           utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    case "bankAccountId":       bankDetailHistory.bankAccountId =       (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "entityNo":            bankDetailHistory.entityNo =            (int)utilitiesHelper.ReadInt32Nullable(data.Value)!; break;
                                    case "effFrom":             bankDetailHistory.effFrom =             (DateTime)utilitiesHelper.ReadDateTimeNullable(data.Value)!; break;
                                    case "effTo":               bankDetailHistory.effTo =               (DateTime)utilitiesHelper.ReadDateTimeNullable(data.Value)!; break;
                                    case "audModifyDate":       bankDetailHistory.audModifyDate =       (DateTime)utilitiesHelper.ReadDateTimeNullable(data.Value)!; break;
                                    case "audModifyUser":       bankDetailHistory.audModifyUser =       utilitiesHelper.ReadStringNullable(data.Value)!; break;
                                    default: TestContext.Out.WriteLine($"Unknown property in data: {data.Name}"); break;
                                }
                            }
                            getBankDetailHistoryResponse.data.Add(bankDetailHistory);
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return getBankDetailHistoryResponse;
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}