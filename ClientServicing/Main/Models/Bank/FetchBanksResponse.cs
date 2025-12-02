using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class FetchBanksResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<FetchBanksRequest> data { get; set; }
    }
}
