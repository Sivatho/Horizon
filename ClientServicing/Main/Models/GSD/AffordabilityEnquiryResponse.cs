using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.GSD
{
    public class AffordabilityEnquiryResponse
    {
        public bool isValid { get; set; }
        public string errorMessage { get; set; }
        public DateTime createdTimestamp { get; set; }
        public string requestId { get; set; }
        public string identityNumber { get; set; }
        public double amount { get; set; }
        public string initials { get; set; }
        public string surname { get; set; }
        public int errorCodeId { get; set; }
        public int errorCode { get; set; }
        public string correlationId { get; set; }
        public string employeeNumberHash { get; set; }
    }
}
