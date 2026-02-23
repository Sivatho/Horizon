using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AccountHistory;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory
{
    public class PolicyCashReceiptValidatiomMethods : AbstractValidationMethods, IPolicyCashReceiptValidatiomMethods
    {
        UtilitiesHelper utilitiesHelper = new();

        public void ValidatePolicyCashReceiptRequestPayload(PolicyAccountHistoryRequest accountHistoryRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(accountHistoryRequest.policyNo,             Is.Not.LessThan(0),             "Response: PolicyNo Should Not Be Less Than 0");
                Assert.That(accountHistoryRequest.legacyPolicyNumber,   Is.Not.Null.And.Not.Empty,      "Response: LegacyPolicyNumber should not be null or empty");
                Assert.That(accountHistoryRequest.auditToken,           Is.Null.Or.TypeOf<string>(),    "Response: AuditToken should not be null or empty");
            });
            DocumentTemplate.DisplayBody("Validate: PolicyAccountHistoryRequest Data Has Valid Property Values");
        }

        public void ValidatePolicyCashReceiptResponsePayload(PolicyCashReceiptResponse policyCashReceiptResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(policyCashReceiptResponse.executionOutcome, Is.Not.Null, "Response: ExecutionOutcome should not be null");
                Assert.That(policyCashReceiptResponse.data,             Is.Not.Null, "Response: Data should not be null");

                Assert.That(policyCashReceiptResponse.executionOutcome.succeeded,   Is.True.Or.False,               "Response: ExecutionOutcome.Succeeded Should Be True Or False");
                Assert.That(policyCashReceiptResponse.executionOutcome.message,     Is.Null.Or.TypeOf<string>(),    "Response: ExecutionOutcome.Message Should Be NUll or Type of String");
                Assert.That(policyCashReceiptResponse.executionOutcome.errors,      Is.Null.Or.TypeOf<string>(),    "Response: ExecutionOutcome.Errors Should Be NUll or Type of String");   
                Assert.That(policyCashReceiptResponse.data, Is.All.Matches<PolicyCashReceipt>(receipt =>
                    receipt.policyNo >= 0 &&
                    !string.IsNullOrEmpty(receipt.reference) &&
                    receipt.billingPeriod > 0 &&
                    receipt.raisedDate != default(DateTime) &&
                    !string.IsNullOrEmpty(receipt.mandateType) &&
                    !string.IsNullOrEmpty(receipt.description) &&
                    receipt.premium >= 0 &&
                    (receipt.susTransTotal == null || receipt.susTransTotal >= 0)
                ), "Response: Each PolicyCashReceipt in Data should have valid property values");
            });
            DocumentTemplate.DisplayBody("Validate: PolicyCashReceiptResponse Data Has Valid Property Values");
        }
        public PolicyCashReceiptResponse PopulatePolicyCashReceiptResponse(RestResponse restResponse)
        {
           using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;
            PolicyCashReceiptResponse policyCashReceiptResponse = new PolicyCashReceiptResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new List<PolicyCashReceipt>()
            };
            foreach (var property in root.EnumerateObject()) { 
                switch(property.Name)
                {
                    case "succeeded":   policyCashReceiptResponse.executionOutcome.succeeded =  (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     policyCashReceiptResponse.executionOutcome.message =    utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      policyCashReceiptResponse.executionOutcome.errors =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data" :
                        var listOfPolicyCashReceipts = new List<PolicyCashReceipt>();
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            var policyCashReceipt = new PolicyCashReceipt();
                            foreach (var itemProperty in item.EnumerateObject())
                            {
                                switch (itemProperty.Name)
                                {
                                    case "policyNo":        policyCashReceipt.policyNo =        (int)utilitiesHelper.ReadInt32Nullable(itemProperty.Value); break;
                                    case "reference":       policyCashReceipt.reference =       utilitiesHelper.ReadStringNullable(itemProperty.Value); break;
                                    case "billingPeriod":   policyCashReceipt.billingPeriod =   (int)utilitiesHelper.ReadInt32Nullable(itemProperty.Value); break;
                                    case "raisedDate":      policyCashReceipt.raisedDate =      (DateTime)utilitiesHelper.ReadDateTimeNullable(itemProperty.Value); break;
                                    case "mandateType":     policyCashReceipt.mandateType =     utilitiesHelper.ReadStringNullable(itemProperty.Value); break;
                                    case "description":     policyCashReceipt.description =     utilitiesHelper.ReadStringNullable(itemProperty.Value); break;
                                    case "premium":         policyCashReceipt.premium =         (int)utilitiesHelper.ReadInt32Nullable(itemProperty.Value); break;
                                    case "susTransTotal":   policyCashReceipt.susTransTotal =   utilitiesHelper.ReadInt32Nullable(itemProperty.Value); break;
                                    default: TestContext.Out.WriteLine($"Unknown property in data: {itemProperty.Name}"); break;
                                }
                            }
                            listOfPolicyCashReceipts.Add(policyCashReceipt);
                        }
                        policyCashReceiptResponse.data = listOfPolicyCashReceipts;
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return policyCashReceiptResponse;
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
