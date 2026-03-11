namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class AddAdjustementToBillingsRequest
    {
        public BillingsAdjustmentInformationRequest billingsAdjustmentInformation { get; set; }
        public BillingAdjustmentPeriodsRequest[] billingAdjustmentPeriods { get; set; }
    }
    public class BillingsAdjustmentInformationRequest
    {
        public int policyNo { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime adjustmentDateFrom { get; set; }
        public Double adjustmentAmount { get; set; }
        public Double totalAdjAmount { get; set; }
        public int adjustedMonthCnt { get; set; }
        public DateTime adjustmentEndDate { get; set; }
        public string comment { get; set; }
        public string actionID { get; set; }
    }
    public class BillingAdjustmentPeriodsRequest
    {
        public int? policyNo { get; set; }
        public string? legacyPolNo { get; set; }
        public string referenceNO { get; set; }
        public int billingPeriod { get; set; }
        public DateTime raisedDate { get; set; }
        public string mandateType { get; set; }
        public string paymentType { get; set; }
        public Double premiumAmount { get; set; }
        public Double amountPaid { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
