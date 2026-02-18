using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy.DBModels
{
    public class PolicyTable
    {
        public int Policy_ID { get; set; }
        public int Policy_NO { get; set; }
        public string? Legacy_Pol_No { get; set; }
        public int Product_Line_CD { get; set; }
        public DateTime? Billed_to { get; set; }
        public DateTime? Paid_to { get; set; }
        public int Billing_Period { get; set; }
        public int PremiumCount { get; set; }
        public int UnMetCount { get; set; }
        public int SeqUnmetCount { get; set; }
        public int Scheme_CD { get; set; }
        public Int32 PolicyGroupingCD { get; set; }
        public string? Aud_Create_User { get; set; }
        public DateTime? Aud_Create_Date { get; set; }
    }
}
