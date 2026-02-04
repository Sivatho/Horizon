namespace ClientServicing.Main.Models.General
{
    public class PolicyIdentifierRequest
    {
        public int policyNo { get; set; }
        public string? legacyPolicyNumber { get; set; }
        public string? auditToken { get; set; }
    }
}
