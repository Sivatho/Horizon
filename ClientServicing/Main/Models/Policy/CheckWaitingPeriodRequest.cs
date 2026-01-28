using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class CheckWaitingPeriodRequest
    {
        public int PolicyNumber { get; set; }
        public int claimCategoryCd { get; set; }
        public int insuredLifeEntityNo { get; set; }
    }
}
