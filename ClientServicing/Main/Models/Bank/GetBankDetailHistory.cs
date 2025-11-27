using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class GetBankDetailHistory
    {
        public int debitDay { get; set; }
        public int paymentMethod { get; set; }
        public string bankAccHolder { get; set; }
        public int bankId { get; set; }
        public string bankName { get; set; }
        public int bankAccTypeCd { get; set; }
        public string bankAccTypeDescr { get; set; }
        public string branchCode { get; set; }
        public string bankAccNo { get; set; }
        public int bankAccountId { get; set; }
        public int entityNo { get; set; }
        public string effFrom { get; set; }
        public string effTo { get; set; }
        public string audModifyDate { get; set; }
        public string audModifyUser { get; set; }
    }
}
