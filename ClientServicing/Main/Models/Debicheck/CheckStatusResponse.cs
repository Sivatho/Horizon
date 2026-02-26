namespace ClientServicing.Main.Models.Debicheck
{
    public class CheckStatusResponse
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<CheckStatusResponseResult>? result { get; set; }
    }
    public class CheckStatusResponseResult {
        public bool success { get; set; }
        public List<CheckStatusResponseData>? data { get; set; }
        public string? message { get; set; }
    }
    public class CheckStatusResponseData
    {
        public int amount { get; set; }
        public bool ifaBusinessFeeIncluded { get; set; }
        public bool success { get; set; }
        public string? message { get; set; }
        public string? status { get; set; }
        public string? payerIdentityNumber { get; set; }
        public string? payerMobileTelephoneNumber { get; set; }
        public string? policyNumber { get; set; }
        public DateTime createdAt { get; set; }
        public string? mandateType { get; set; }

    }
}