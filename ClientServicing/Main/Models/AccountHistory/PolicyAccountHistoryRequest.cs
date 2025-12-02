using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class PolicyAccountHistoryRequest
    {
        public int? bankID { get; set; }
        public string? bankName { get; set; }
        public string? bankShortName { get; set; }
        public int? dispSeq { get; set; }
        public bool? isActive { get; set; }
        public DateTime? lastChanged { get; set; }
        public string? userID { get; set; }
    }
}
