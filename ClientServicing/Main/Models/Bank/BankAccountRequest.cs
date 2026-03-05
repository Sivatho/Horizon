using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class BankAccountRequest
    {
        public int bankAccountID { get; set; }
        public List<int> bankAccountList { get; set; }
    }
}
