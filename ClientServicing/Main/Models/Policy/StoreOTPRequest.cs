using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class StoreOTPRequest
    {
        public int policyNo { get; set; }
        public string managerName { get; set; }
        public string managerEmail { get; set; }
        public string otp { get; set; }
    }
}
