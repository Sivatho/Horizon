using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.InputTypes;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class GetInsuredWithBenefitResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<BenefitData> data { get; set; }
    }
    public class BenefitData {
        public int benefitID{ get; set; }
        public double benefitCover { get; set; }
    }
}
