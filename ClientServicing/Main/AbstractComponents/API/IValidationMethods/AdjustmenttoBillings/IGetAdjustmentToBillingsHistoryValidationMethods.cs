using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings
{
    internal interface IGetAdjustmentToBillingsHistoryValidationMethods
    {
        public void ValidateGetAdjustmentToBillingsHistoryResponseDataIsNotNullOrEmpty(GetAdjustmentToBillingsHistoryValidationMethod getadjustmenttobillingshistory);
    }
}
