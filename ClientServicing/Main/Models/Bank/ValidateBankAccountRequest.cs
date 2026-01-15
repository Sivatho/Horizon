namespace ClientServicing.Main.Models.Bank
{
    public class ValidateBankAccountRequest
    {
        public string idNumber { get; set; }
        public string bankAccountHolderInitials { get; set; }
        public string surname { get; set; }
        public string bankName { get; set; }
        public string bankAccountNumber { get; set; }
        public string bankBranchCode { get; set; }
        public string bankAccountType { get; set; }
    }
}
