using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Email
{
    public class SendInternalEmailsRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
    public class SendInternalEmailsResponse { 
        public string responseString { get; set; }
    }
}
