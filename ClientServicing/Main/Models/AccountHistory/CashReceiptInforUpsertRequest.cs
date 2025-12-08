using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class CashReceiptInforUpsertRequest
    {
        public int policyNo { get; set; }
        public String legacyPolicyNumber { get; set; }
        public String cashReceiptInformation { get; set; }
    }
}
