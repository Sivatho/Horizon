namespace ClientServicing.Main.Models.Debicheck
{
    public class DebicheckRetryCheckStatusRequest
    {
        public string policyNumber { get; set; }
        public string? identityNumber { get; set; }
        public string? cellPhoneNumber { get; set; }
        public string? accountNumber { get; set; }
        public string? branchCode { get; set; }
        public string? accountType { get; set; }
        public string? bankName { get; set; }
        public string? surnameOrCompanyName { get; set; }
        public string? initials { get; set; }
        public int? amount { get; set; }
        public bool bypassD3Check { get; set; }
        public int sourceSystemId { get; set; } 

    }
    public class CheckStatusRequestData {
        public List<DebicheckRetryCheckStatusRequest>? listOdCheckStatusRequest { get; set; }
    }
}