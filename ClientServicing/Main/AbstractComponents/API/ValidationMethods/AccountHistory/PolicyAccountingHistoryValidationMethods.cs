using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AccountHistory;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory
{
    public class PolicyAccountingHistoryValidationMethods : AbstractValidationMethods, IPolicyAccountingHistoryValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidatePolicyAccountingHistorResponseDataIsNotNullOrEmpty(PolicyAccountHistoryResponse policyAccountHistoryResponse)
        {
            PolicyAccountHistoryResponseData data = new PolicyAccountHistoryResponseData();
            //Assert
            Assert.Multiple(() =>
            {
                //Response Content
                Assert.That(policyAccountHistoryResponse.executionOutcome,  Is.Not.Null.Or.Empty, "Response: executionOutcome Should Not Be Null Or Empty");
                Assert.That(policyAccountHistoryResponse.data,              Is.Not.Null.Or.Empty, "Response: Data Should Not Be Null Or Empty");

                //Validate Each Object Data Types
                Assert.That(policyAccountHistoryResponse.executionOutcome.succeeded,    Is.TypeOf<bool>(),              "Response: Succeeded Should Be Bool.");
                Assert.That(policyAccountHistoryResponse.executionOutcome.message,      Is.Null.Or.TypeOf<string?>(),   "Response: Message Should Be Null Or Type Of String.");
                Assert.That(policyAccountHistoryResponse.executionOutcome.errors,       Is.Null.Or.TypeOf<string?>(),   "Response: Errors Should Be Null Or Type Of String.");

                Assert.That(policyAccountHistoryResponse.data.accountingHistoryPaymentResults,  Is.Not.Null.Or.Empty,       "Response: accountingHistoryPaymentResults Should Not Be NUll Or Empty");
                Assert.That(policyAccountHistoryResponse.data.accountingHistoryPolicyResults,   Is.Not.Null.And.Not.Empty,  "Policy results list should not be null or empty.");

                var payments = policyAccountHistoryResponse.data.accountingHistoryPaymentResults;
                Assert.That(payments.totalNumberOfPayments,     Is.TypeOf<int>(),               "Response: TotalNumberOfPayments Should Be A Type Integer");
                Assert.That(payments.totalAmountReceived,       Is.TypeOf<double>(),            "Response: TotalAmountOutstanding Should Be A Type Integer");
                Assert.That(payments.totalAmountOutstanding,    Is.TypeOf<double>(),            "Response: TotalAmountReceived Should Be A Type Integer");
                Assert.That(payments.collectionMethod,          Is.Not.Null.Or.Empty,           "Response: CollectionMethod Should Not Be Null Or Empty");
                Assert.That(payments.mandateType,               Is.Null.Or.TypeOf<string?>(),   "Response: MandateType Should Be Null Or Be Type String");
                Assert.That(payments.gsdType,                   Is.TypeOf<int>(),               "Response: GSDType Should Be A Type Integer");
                Assert.That(payments.suspenseAmt,               Is.TypeOf<double>(),            "Response: SuspenseAmt Should Be A Type Double");                
                
                foreach (var item in policyAccountHistoryResponse.data.accountingHistoryPolicyResults) {
                    Assert.That(item.policyNo,                  Is.Not.LessThan(0),                         "Response: Policy Should Not Be Less Than 0");
                    Assert.That(item.legacy_Pol_No,             Is.Not.Null.Or.Not.Empty,                   "Response: Legacy_Pol_No Should Not Be Null Or Empty");
                    Assert.That(item.referenceNO,               Is.Not.Null.Or.Not.Empty,                   "Response: ReferenceNO Should Not Be Null Or Empty");
                    Assert.That(item.month,                     Is.Not.LessThan(0),                         "Response: Policy Should Not Be Less Than Zero");
                    Assert.That(item.raisedDate,                Is.Not.EqualTo(default(DateTime)),          "Response: RaisedDate Should Not Be Equal To Default DateTime");
                    Assert.That(item.bankSubmissionDate,        Is.Null.Or.Not.EqualTo(default(DateTime)),  "Response: BankSubmissionDate Should Not Be Null Or Not Equal To Default DateTime");
                    Assert.That(item.strikeDate,                Is.Null.Or.Not.EqualTo(default(DateTime)),  "Response: StrikeDate Should Not Be Null Or Not Equal To Default DateTime");
                    Assert.That(item.paymentDate,               Is.Not.EqualTo(default(DateTime)),          "Response: PaymentDate Should Not Equal To Default DateTime");
                    Assert.That(item.trackingDays,              Is.Null.Or.TypeOf<int?>(),                  "Response: TrackingDays Should Be Null Or Type Of Integer");
                    Assert.That(item.mandateType,               Is.Null.Or.TypeOf<string>(),                "Response: MandateType Should Be Null Or Type Of Integer");
                    Assert.That(item.paymentType,               Is.Not.Null.Or.Not.Empty,                   "Response: PaymentType Should Not Be Null Or Empty");
                    Assert.That(item.description,               Is.Not.Null.Or.Not.Empty,                   "Response: Description Should Not Be Null Or Empty");
                    Assert.That(item.premiumAmount,             Is.Not.LessThan(0),                         "Response: PremiumAmount Should Not Be Less Than 0");
                    Assert.That(item.amountPaid,                Is.TypeOf<double>(),                        "Response: AmountPaid Should Be Of Type Double");
                }
            });
            DocumentTemplate.DisplayBody("Validated: PolicyAccountHistoryResponse: Is Not Null Or Empty And Integer Is Not Less Than 0 And DateTime Is Not Equal To Default");
        }

        public void ValidatePolicyAccountingHistoryRequestDataIsNotNullOrEmpty(PolicyAccountHistoryRequest policyAccountHistoryRequest)
        {
            Assert.Multiple(() => {
                Assert.That(policyAccountHistoryRequest,                    Is.Not.Null.Or.Empty,                   "Response: policyAccountHistoryRequest Should Not Be Null Or Empty");
                Assert.That(policyAccountHistoryRequest.policyNo,           Is.Not.LessThan(0),                     "Response: policyNo Should Not Be Less Than 0");
                Assert.That(policyAccountHistoryRequest.legacyPolicyNumber, Is.Null.Or.Empty.Or.TypeOf<string?>(),  "Response: legacyPolicyNumber Should Be Null Or Empty Or Type Of String");
                });
            DocumentTemplate.DisplayBody("Validated: PolicyAccountHistoryRequest: Is Not Be Null Or Empty, Type Of Integer Is True, Integer Is Not Less Than 0");
        }
        public PolicyAccountHistoryResponse PopulatePolicyAccountHistoryResponse(RestResponse response) {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var policyAccountHistoryResponse = new PolicyAccountHistoryResponse { 
                executionOutcome = new ExecutionOutcome(),
                data = new PolicyAccountHistoryResponseData()
            };

            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": policyAccountHistoryResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": policyAccountHistoryResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": policyAccountHistoryResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "accountingHistoryPaymentResults":
                                    var paymentResult = new AccountingHistoryPaymentResults();
                                    foreach (var payment in item.Value.EnumerateObject()) {
                                        switch (payment.Name) {
                                            case "totalNumberOfPayments":   paymentResult.totalNumberOfPayments =   (int)utilitiesHelper.ReadInt32Nullable(payment.Value); break;
                                            case "totalAmountReceived":     paymentResult.totalAmountReceived =     (double)utilitiesHelper.ReadInt32Nullable(payment.Value); break;
                                            case "totalAmountOutstanding":  paymentResult.totalAmountOutstanding =  (double)utilitiesHelper.ReadInt32Nullable(payment.Value); break;
                                            case "collectionMethod":        paymentResult.collectionMethod =        utilitiesHelper.ReadStringNullable(payment.Value); break;
                                            case "mandateType":             paymentResult.mandateType =             utilitiesHelper.ReadStringNullable(payment.Value); break;
                                            case "gsdType":                 paymentResult.gsdType =                 (int)utilitiesHelper.ReadInt32Nullable(payment.Value); break;
                                            case "suspenseAmt":             paymentResult.suspenseAmt =             (double)utilitiesHelper.ReadInt32Nullable(payment.Value); break;
                                            default: TestContext.Out.WriteLine($"Unknown property in data: {payment.Name}"); break;
                                        }  
                                        policyAccountHistoryResponse.data.accountingHistoryPaymentResults = paymentResult;
                                    }
                                    break;                                
                                case "accountingHistoryPolicyResults":
                                    var list = new List<AccountingHistoryPolicyResults>();
                                    foreach (var policy in item.Value.EnumerateArray()) {
                                        var policyResult = new AccountingHistoryPolicyResults();
                                        foreach (var p in policy.EnumerateObject()) {
                                            switch (p.Name) {
                                                case "policyNo":            policyResult.policyNo =                 (int)utilitiesHelper.ReadInt32Nullable(p.Value); break;
                                                case "legacy_Pol_No":       policyResult.legacy_Pol_No =            utilitiesHelper.ReadStringNullable(p.Value); break; 
                                                case "referenceNO":         policyResult.referenceNO =              utilitiesHelper.ReadStringNullable(p.Value); break;
                                                case "month":               policyResult.month =                    (int)utilitiesHelper.ReadInt32Nullable(p.Value); break;
                                                case "raisedDate":          policyResult.raisedDate =               (DateTime)utilitiesHelper.ReadDateTimeNullable(p.Value); break;
                                                case "bankSubmissionDate":  policyResult.bankSubmissionDate =       utilitiesHelper.ReadDateTimeNullable(p.Value); break;
                                                case "strikeDate":          policyResult.strikeDate =               utilitiesHelper.ReadDateTimeNullable(p.Value); break;
                                                case "paymentDate":         policyResult.paymentDate =              (DateTime)utilitiesHelper.ReadDateTimeNullable(p.Value); break;
                                                case "trackingDays":        policyResult.trackingDays =             utilitiesHelper.ReadInt32Nullable(p.Value); break;
                                                case "mandateType":         policyResult.mandateType =              utilitiesHelper.ReadStringNullable(p.Value); break;
                                                case "paymentType":         policyResult.paymentType =              utilitiesHelper.ReadStringNullable(p.Value); break;
                                                case "description":         policyResult.description =              utilitiesHelper.ReadStringNullable(p.Value); break;
                                                case "premiumAmount":       policyResult.premiumAmount =            (double)utilitiesHelper.ReadInt32Nullable(p.Value); break;
                                                case "amountPaid":          policyResult.amountPaid =               (double)utilitiesHelper.ReadInt32Nullable(p.Value); break;
                                                default: TestContext.Out.WriteLine($"Unknown property in data: {p.Name}"); break;
                                            }
                                        }
                                        list.Add(policyResult);
                                    }
                                    policyAccountHistoryResponse.data.accountingHistoryPolicyResults = list.ToArray();
                                    break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return policyAccountHistoryResponse;
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
