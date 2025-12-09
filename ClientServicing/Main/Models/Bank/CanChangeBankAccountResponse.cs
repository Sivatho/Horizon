using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class CanChangeBankAccountResponse
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string errors { get; set; }
        public CompleteStatusMessages data { get; set; }
    }
}
