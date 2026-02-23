using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class StatementLineIDRequest
    {
        public string statementLineID { get; set; }
        public int policyNo { get; set; }
        public int transCatCD { get; set; }
    }
}
