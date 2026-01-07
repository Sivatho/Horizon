using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class PersonSearchRequest
    {
        public string policyNo { get; set; }
        public string legPolNo { get; set; }
        public string clientEntityNo { get; set; }
        public string legalRefNo { get; set; }
        public string claimNo { get; set; }
        public string cellNo { get; set; }
        public string emailAddress { get; set; }
        public string fullName { get; set; }
        public string inspiratorNo { get; set; }
        public string voucherNo { get; set; }
        public int partnerCD { get; set; }
        public string auditToken { get; set; }
    }
}
