using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class GetInsuredWithBenefitRequest
    {
        public int policyNo { get; set; }
        public int insuredlifeEntityNo { get; set; }
    }
}