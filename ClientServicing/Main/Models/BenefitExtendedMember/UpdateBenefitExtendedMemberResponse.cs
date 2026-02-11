using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.BenefitExtendedMember
{
    public class updateBenefitExtendedMemberResponse
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string? error { get; set; }
        public List<policyBenefitExtendedMemberRequest> data { get; set; }

    }
}