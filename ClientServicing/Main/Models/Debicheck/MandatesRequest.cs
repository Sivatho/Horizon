using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Debicheck
{
    public class MandatesRequest
    {
        public required string? policyNumber { get; set; }
        public bool existingClient { get; set; }
        public string? payerMobileTelephoneNumber { get; set; }
        public int sourceSystemId { get; set; }
        public string? agentCode { get; set; }
        public string? agentName { get; set; }
        public required int transactionType { get; set; }




    }
}
