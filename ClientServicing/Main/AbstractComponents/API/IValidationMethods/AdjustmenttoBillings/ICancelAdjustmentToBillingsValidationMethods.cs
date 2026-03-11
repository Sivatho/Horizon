using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Models.AdjustementToBillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings
{
    public interface ICancelAdjustmentToBillingsValidationMethods
    {

        public void ValidatCancelAdjustmentToBillingsRequestDataIsNotNullOrEmpty(CancelAdjustmentToBillingsRequest canceladjustmenttobillings);
        public void ValidateResponseFieldParametersIsValid(CancelAdjustmentToBillingsResponse canceladjustmenttobillingsresponse);

    }
}
