namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class GetOutstandingPolicyPremiumsRequest
    {
        public int policyNo { get; set; }
        public String legacyPolicyNumber { get; set; }
        public String auditToken { get; set; }

    }
}
