namespace ClientServicing.Main.Models.Payer
{
    public class UpsertBankingAndPayerRequest
    {
        public int policyNo { get; set; }
        public int titleId { get; set; }
        public string initials { get; set; }
        public int entityNo { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
        public int legalRefType { get; set; }
        public string legalRefNo { get; set; }
        public int relationCd { get; set; }
        public string dateOfBirth { get; set; }
        public int genderId { get; set; }
        public string cellNumber { get; set; }
        public string homeNumber { get; set; }
        public string workNumber { get; set; }
        public string emailAddress { get; set; }
        public string employerName { get; set; }
        public string employerNumber { get; set; }
        public int paymentMethod { get; set; }
        public int currentPaymentMethod { get; set; }
        public int paymentFreqCD { get; set; }
        public int deductDay { get; set; }
        public DateTime firstDebitDate { get; set; }
        public int paymentRefId { get; set; }
        public int bankAccTypeCD { get; set; }
        public int bankId { get; set; }
        public string bankName { get; set; }
        public string bankAccNo { get; set; }
        public string bankAccHolderInitial { get; set; }
        public string bankAccHolder { get; set; }
        public int branchNo { get; set; }
        public string bankAccBranchCode { get; set; }
        public DateTime bankAccStartDate { get; set; }
        public DateTime bankAccEndDate { get; set; }
        public bool isActive { get; set; }
        public DateTime effectiveDate { get; set; }
        public string userID { get; set; }
        public int gsdDepartmentCd { get; set; }
        public string gsdDepartmentName { get; set; }
        public string gsdEmployeeNumber { get; set; }
        public string csdCompanyName { get; set; }
        public string csdDepartmentName { get; set; }
        public string csdEmployeeNumber { get; set; }
        public bool earlyTracking { get; set; }
        public int citizenShipCD { get; set; }
        public double premium { get; set; }
        public bool hasBillingRequestChanged { get; set; }
        public string payroll { get; set; }
        public string mandateType { get; set; }
        public bool isAuthorized { get; set; }
        public string userName { get; set; }
        public string auditToken { get; set; }
    }
}
