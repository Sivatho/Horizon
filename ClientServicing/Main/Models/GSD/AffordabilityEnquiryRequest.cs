using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.GSD
{
    public class AffordabilityEnquiryRequest
    {
        public string requestId { get; set; }
        public string employeeNumber { get; set; }
        public string payrollNumber { get; set; }
        public double amount { get; set; }
        public string initials { get; set; }
        public string surname { get; set; }
        public string identityNumber { get; set; }
        public int productCategory { get; set; }
        public string department { get; set; }
        public int sourceSystem { get; set; }
    }
}
