using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class GetBankDetailHistoryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<GetBankDetailHistory> data { get; set; }
    }
}
