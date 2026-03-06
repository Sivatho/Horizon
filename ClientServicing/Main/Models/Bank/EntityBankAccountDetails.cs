using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class EntityBankAccountDetails
    {
        public int BankAccountID { get; set; }
        public int EntityNo { get; set; }
        public int BankAccTypeCD { get; set; }
        public int BankID { get; set; }
        public string BankAccNo { get; set; }
        public string? BankAccHolderInitial { get; set; }
        public string? BankAccHolderName { get; set; }
        public string? BankAccHolderSurname { get; set; }
        public string? BranchNo { get; set; }
        public string? BankAccSwiftCode { get; set; }
        public DateTime? EffFrom { get; set; }
        public DateTime? EffTo { get; set; }
        public int StatusCD { get; set; }
        public DateTime? StatusDate { get; set; }
        public string AudCreateUser { get; set; }
        public string? AudCreateDate { get; set; }
        public string AudModUser { get; set; }
        public string AudModDate { get; set; }

    }
}
