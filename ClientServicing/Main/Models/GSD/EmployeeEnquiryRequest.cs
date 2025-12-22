using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.GSD
{
    public class EmployeeEnquiryRequest
    {
        public string requestId { get; set; }
        public string employeeNumber { get; set; }
        public string department { get; set; }
        public int sourceSystem { get; set; }
    }
}
