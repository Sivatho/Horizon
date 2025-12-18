using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class UpdatePolicyBenefitciaryResponse
    {
        public bool succeeded { get; set; }
        public string? message { get; set; }
        public string? errors { get; set; }
        public bool data { get; set; }
    }
}
