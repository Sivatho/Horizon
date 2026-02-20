using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistoryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public PolicyAccountHistoryResponseData data { get; set; }
    }
    public class PolicyAccountHistoryResponseData {
        public AccountingHistoryPaymentResults accountingHistoryPaymentResults { get; set; }
        public AccountingHistoryPolicyResults[] accountingHistoryPolicyResults { get; set; }
    }
    public class AccountingHistoryPaymentResults {
        public int totalNumberOfPayments { get; set; }
        public double totalAmountReceived { get; set; }
        public double totalAmountOutstanding { get; set; }
        public string collectionMethod { get; set; }
        public string? mandateType { get; set; }
        public int gsdType { get; set; }
        public double suspenseAmt { get; set; }
    }
    public class AccountingHistoryPolicyResults {
        public int policyNo { get; set; }
        public string legacy_Pol_No { get; set; }
        public string referenceNO { get; set; }
        public int month { get; set; }
        public DateTime? raisedDate { get; set; }
        public DateTime? bankSubmissionDate { get; set; }
        public DateTime? strikeDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? trackingDays { get; set; }
        public string? mandateType { get; set; }
        public string paymentType { get; set; }
        public string description { get; set; }
        public double premiumAmount { get; set; }
        public double amountPaid { get; set; }
    }
}
