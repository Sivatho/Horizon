using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.RemovalFromBillings
{
    public class RemovalFromBillingsRequest
    {
        public int policyNo { get; set; }
        public int? period { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime endDate { get; set; }
        public string? comment { get; set; }
        public int? removeID { get; set; }
        public string? userID { get; set; }
    }
}
