using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyCashRecieptRequest
    {
     
        public int totalPolicies { get; set; }
        public bool limitExceeded { get; set; }
    }
}
