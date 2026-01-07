using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class AdvancedPersonSearchRequest
    {
        public string encashmentNo { get; set; }
        public string busRegNo { get; set; }
        public string appFormNo { get; set; }
        public string emailAddress { get; set; }
        public string businessName { get; set; }
        public string fullName { get; set; }
        public string inspiratorNo { get; set; }
        public string worksiteNo { get; set; }
    }
}
