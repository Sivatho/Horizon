using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class ValidateBankAccountQAVSRRequest
    {
        public int payerEntityNo { get; set; }
        public string policyNumber { get; set; }
        public string productCategory { get; set; }
        public int productCategoryId { get; set; }
        public string title { get; set; }
        public int titleCd { get; set; }
        public string firstName { get; set; }        
        public string surname { get; set; }
        public string initials { get; set; }
        public string emailAddress { get; set; }
        public bool isAMember { get; set; }
        public string legalRefNo { get; set; }
        public int legalRefNoTypeCD { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int genderCd { get; set; }
        public string employerName { get; set; }
        public string employeeNumber { get; set; }
        public string employeeDepartmentCd { get; set; }
        public string employeeDepartment { get; set; }
        public int authorizationTypeCd { get; set; }
        public string payroll { get; set; }
        public string mandateType { get; set; }
        public string agentID { get; set; }
        public bool isAuthorized { get; set; }
        public string homeNumber { get; set; }
        public string cellNumber { get; set; }
        public string workNumber { get; set; }
        public int relationCd { get; set; }
        public int bankId { get; set; }
        public string bankName { get; set; }
        public string bankShortName { get; set; }
        public int branchNo { get; set; }
        public string branchCode { get; set; }
        public string bankAccNo { get; set; }
        public string bankAccHolderInitial { get; set; }
        public string bankAccHolderName { get; set; }
        public int bankAccTypeCD { get; set; }
        public string bankAccTypeDesc { get; set; }
        public int bankAccountID { get; set; }
        public string bankAccSwiftCode { get; set; }
        public int isActive { get; set; }
        public int paymentTypeCD { get; set; }
        public int paymentFreqCD { get; set; }
        public int paymentRefId { get; set; }
        public int premium { get; set; }
        public bool earlyTracking { get; set; }
        public int debitDay { get; set; }
        public DateTime firstDebitDay { get; set; }
        public DateTime firstDebitMonth { get; set; }
        public DateTime effectiveDate { get; set; }
        public GSD gsd { get; set; }
        public DateTime lastChanged { get; set; }
        public string userID { get; set; }
        public string entityNo { get; set; }
        public string agentCode { get; set; }
        public string agentName { get; set; }
        public string payAtNumber { get; set; }
        public string csdEmployeeNo { get; set; }
        public string csdCompanyDepartment { get; set; }
        public string csdCompanyName { get; set; }
        public int csdCompanyCd { get; set; }
    }
}
