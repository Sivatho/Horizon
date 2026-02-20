using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AccountHistory;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory
{
    public class PolicyAcountingHistorySummaryValidationMethods : AbstractValidationMethods, IPolicyAcountingHistorySummaryValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidatePolicyAccountHistorySummaryRequestPayload(PolicyAccountHistorySummaryRequest policyAccountHistorySummaryRequest)
        {
            Assert.Multiple(() => {
                Assert.That(policyAccountHistorySummaryRequest.policyNo, Is.Not.LessThan(0), "Response: PolicyNo Should Not Be Less Than 0");
                Assert.That(policyAccountHistorySummaryRequest.billingPeriod, Is.Not.LessThan(0), "Response: BillingPeriod Should Not Be Less Than 0");
            });
            DocumentTemplate.DisplayBody("Validated: PolicyAccountHistorySummaryRequest: Integer Is Not Less Than 0");
        }

        public void ValidatePolicyAccountHistorySummaryResponsePayload(PolicyAccountHistorySummaryResponse policyAccountHistorySummaryResponse)
        {
            Assert.Multiple(() => {
                Assert.That(policyAccountHistorySummaryResponse.executionOutcome,   Is.Not.Null.Or.Empty, "Response: executionOutcome Should Not Be Null Or Empty");
                Assert.That(policyAccountHistorySummaryResponse.data,               Is.Not.Null.Or.Empty, "Response: data Should Not Be Null Or Empty");

                Assert.That(policyAccountHistorySummaryResponse.executionOutcome.succeeded, Is.TypeOf<bool>(),              "Response: Succeeded Should Be Bool.");
                Assert.That(policyAccountHistorySummaryResponse.executionOutcome.message,   Is.Null.Or.TypeOf<string?>(),   "Response: Message Should Be Null Or Type Of String.");
                Assert.That(policyAccountHistorySummaryResponse.executionOutcome.errors,    Is.Null.Or.TypeOf<string?>(),   "Response: Errors Should Be Null Or Type Of String.");

                foreach (var accountingHistoryPolicyResults in policyAccountHistorySummaryResponse.data) {
                    Assert.That(accountingHistoryPolicyResults.policyNo,            Is.Not.LessThan(0),                         "Response: Policy Should Not Be Less Than 0");
                    Assert.That(accountingHistoryPolicyResults.legacy_Pol_No,       Is.Not.Null.Or.Not.Empty,                   "Response: Legacy_Pol_No Should Not Be Null Or Empty");
                    Assert.That(accountingHistoryPolicyResults.referenceNO,         Is.Not.Null.Or.Not.Empty,                   "Response: ReferenceNO Should Not Be Null Or Empty");
                    Assert.That(accountingHistoryPolicyResults.month,               Is.Not.LessThan(0),                         "Response: Policy Should Not Be Less Than Zero");
                    Assert.That(accountingHistoryPolicyResults.raisedDate,          Is.Not.EqualTo(default(DateTime)),          "Response: RaisedDate Should Not Be Equal To Default DateTime");
                    Assert.That(accountingHistoryPolicyResults.bankSubmissionDate,  Is.Null.Or.Not.EqualTo(default(DateTime)),  "Response: BankSubmissionDate Should Not Be Null Or Not Equal To Default DateTime");
                    Assert.That(accountingHistoryPolicyResults.strikeDate,          Is.Null.Or.Not.EqualTo(default(DateTime)),  "Response: StrikeDate Should Not Be Null Or Not Equal To Default DateTime");
                    Assert.That(accountingHistoryPolicyResults.paymentDate,         Is.Not.EqualTo(default(DateTime)),          "Response: PaymentDate Should Not Equal To Default DateTime");
                    Assert.That(accountingHistoryPolicyResults.trackingDays,        Is.Null.Or.TypeOf<int>(),                  "Response: TrackingDays Should Be Null Or Type Of Integer");
                    Assert.That(accountingHistoryPolicyResults.mandateType,         Is.Null.Or.TypeOf<string>(),                "Response: MandateType Should Be Null Or Type Of Integer");
                    Assert.That(accountingHistoryPolicyResults.paymentType,         Is.Not.Null.Or.Not.Empty,                   "Response: PaymentType Should Not Be Null Or Empty");
                    Assert.That(accountingHistoryPolicyResults.description,         Is.Not.Null.Or.Not.Empty,                   "Response: Description Should Not Be Null Or Empty");
                    Assert.That(accountingHistoryPolicyResults.premiumAmount,       Is.Not.LessThan(0),                         "Response: PremiumAmount Should Not Be Less Than 0");
                    Assert.That(accountingHistoryPolicyResults.amountPaid,          Is.TypeOf<double>(),                        "Response: AmountPaid Should Be Of Type Double");
                }
            });
            DocumentTemplate.DisplayBody("Validated: PolicyAccountHistorySummaryResponse: Integer Is Not Less Than 0");
        }

        public PolicyAccountHistorySummaryResponse PopulatePolicyAccountHistorySummaryResponse(RestResponse response) { 
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var policyAccountHistorySummaryResponse = new PolicyAccountHistorySummaryResponse {
                executionOutcome = new Models.General.ExecutionOutcome(),
                data = new List<AccountingHistoryPolicyResults>()
            };            
            foreach (var property in jsDoc.RootElement.EnumerateObject()) {
                switch (property.Name) {
                    case "succeeded" :  policyAccountHistorySummaryResponse.executionOutcome.succeeded =    (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message" :    policyAccountHistorySummaryResponse.executionOutcome.message =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      policyAccountHistorySummaryResponse.executionOutcome.errors =       utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data" :
                        var listOfPolicy = new List<AccountingHistoryPolicyResults>();
                        foreach (var item in property.Value.EnumerateArray()) {
                            var accountingHistoryPolicyResults = new AccountingHistoryPolicyResults();
                            foreach (var policy in item.EnumerateObject()) {                                
                                switch (policy.Name) {
                                    case "policyNo":            accountingHistoryPolicyResults.policyNo =           (int)utilitiesHelper.ReadInt32Nullable(policy.Value); break;
                                    case "legacy_Pol_No":       accountingHistoryPolicyResults.legacy_Pol_No =      utilitiesHelper.ReadStringNullable(policy.Value); break;
                                    case "referenceNO":         accountingHistoryPolicyResults.referenceNO =        utilitiesHelper.ReadStringNullable(policy.Value); break;
                                    case "month":               accountingHistoryPolicyResults.month =              (int)utilitiesHelper.ReadInt32Nullable(policy.Value); break;
                                    case "raisedDate":          accountingHistoryPolicyResults.raisedDate =         (DateTime?)utilitiesHelper.ReadDateTimeNullable(policy.Value); break;
                                    case "bankSubmissionDate":  accountingHistoryPolicyResults.bankSubmissionDate = (DateTime?)utilitiesHelper.ReadDateTimeNullable(policy.Value); break;
                                    case "strikeDate":          accountingHistoryPolicyResults.strikeDate =         (DateTime?)utilitiesHelper.ReadDateTimeNullable(policy.Value); break;
                                    case "paymentDate":         accountingHistoryPolicyResults.paymentDate =        (DateTime?)utilitiesHelper.ReadDateTimeNullable(policy.Value); break;
                                    case "trackingDays":        accountingHistoryPolicyResults.trackingDays =       utilitiesHelper.ReadInt32Nullable(policy.Value); break;
                                    case "mandateType":         accountingHistoryPolicyResults.mandateType =        utilitiesHelper.ReadStringNullable(policy.Value); break;
                                    case "paymentType":         accountingHistoryPolicyResults.paymentType =        utilitiesHelper.ReadStringNullable(policy.Value); break;
                                    case "description":         accountingHistoryPolicyResults.description =        utilitiesHelper.ReadStringNullable(policy.Value); break;
                                    case "premiumAmount":       accountingHistoryPolicyResults.premiumAmount =      (double)utilitiesHelper.ReadInt32Nullable(policy.Value); break;
                                    case "amountPaid":          accountingHistoryPolicyResults.amountPaid =         (double)utilitiesHelper.ReadInt32Nullable(policy.Value); break;
                                    default: TestContext.Out.WriteLine($"Unknown property in data: {policy.Name}"); break;
                                }                                
                            }
                            listOfPolicy.Add(accountingHistoryPolicyResults);
                        }
                        policyAccountHistorySummaryResponse.data = listOfPolicy;
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return policyAccountHistorySummaryResponse;
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
