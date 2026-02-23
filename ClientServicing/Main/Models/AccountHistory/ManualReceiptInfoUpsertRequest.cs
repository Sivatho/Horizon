using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class ManualReceiptInfoUpsertRequest
    {
        public int policyNo { get; set; }
        public int transCatCD { get; set; }
        public DateTime transactionDate { get; set; }
        public double randAmount { get; set; }
        public string paymentDescription { get; set; }
    }
}
