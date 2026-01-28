using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{

    //Claims is spCheckWaitingPeriod no longer used as there is a new project in development for claims
    public class CheckWaitingPeriodRequest
    {
        public int PolicyNumber { get; set; }
        public int claimCategoryCd { get; set; }
        public int insuredLifeEntityNo { get; set; }
    }
}
