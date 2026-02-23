using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using ClientServicing.Main.Models.AdjustementToBillings;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings
{
    internal interface IGetAdjustmentToBillingsHistoryValidationMethods
    {
        public void ValidateGetAdjustmentToBillingsHistoryResponseIsValid(GetAdjustmentToBillingsHistoryResponse response);
    }
}
