using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class GetPolicyProductLineResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public GetPolicyProductLine data { get; set; }
    }
    public class GetPolicyProductLine {
        public int policyNo { get; set; }
        public int productLineCD { get; set; }
        public string productLineDescription { get; set; }
    }
}
