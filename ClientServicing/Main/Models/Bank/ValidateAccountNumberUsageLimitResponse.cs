namespace ClientServicing.Main.Models.Bank
{
    public class ValidateAccountNumberUsageLimitResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int totalPolicies { get; set; }
        public bool limitExceeded { get; set; }
    }
}
