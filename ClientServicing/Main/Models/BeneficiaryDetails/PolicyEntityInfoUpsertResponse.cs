using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class PolicyEntityInfoUpsertResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public bool data { get; set; }
    }
}
