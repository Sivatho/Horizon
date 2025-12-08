using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistorySummaryRequest
    {
        public int policyNo { get; set; }
        public String billingPeriod { get; set; } = DateTime.Now.ToString("yyMM");

    }
}
