using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class PolicyBeneficiaryDetailsResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public BeneficiaryDetailsData data { get; set; }
    }
}
