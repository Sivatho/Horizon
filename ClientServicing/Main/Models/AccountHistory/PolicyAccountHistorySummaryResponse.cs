using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class PolicyAccountHistorySummaryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<GetBankDetailHistory> data { get; set; }
    }
}
