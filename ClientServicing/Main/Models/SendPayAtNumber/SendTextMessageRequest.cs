using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.SendPayAtNumber
{
    public class SendTextMessageRequest
    {
        public string? mobileTelephoneNumber { get; set; }
        public string? subAffiliateCode { get; set; }
        public string? textMessageContent { get; set; }
        public VariableDto<string, string>? variables { get; set; }
        public string? platform { get; set; }
        public string brand { get; set; }
        public string? optionalReason { get; set; }
        public bool optionalBusinessHoursOnly { get; set; }
        public string? optionalCostCode { get; set; }
        public string? responseGuid { get; set; }
    }
    public class VariableDto<TKey, TValue> {
        public string? key { get; set; }
        public string? value { get; set; }
    }
}
