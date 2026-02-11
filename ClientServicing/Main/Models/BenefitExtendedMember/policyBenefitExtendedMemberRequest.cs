using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BenefitExtendedMember
{
    public class policyBenefitExtendedMemberRequest
    {
        public int PolicyNo { get; set; }  // or add JsonConverter to convert string to int
        public DateTime? effectiveDate { get; set; }

    }
}
