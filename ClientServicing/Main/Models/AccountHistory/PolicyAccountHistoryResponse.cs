using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class PolicyAccountHistoryResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<PolicyAccountHistoryRequest> data { get; set; }
    }
}
