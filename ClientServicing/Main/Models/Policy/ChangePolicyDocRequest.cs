using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class ChangePolicyDocRequest
    {
        public string policyNo { get; set; }
        public string currentdocdate { get; set; }
        public string changetype { get; set; }
        public string newdocdate { get; set; }
        public int newdebitdate { get; set; }
        public string comment { get; set; }
    }
}
