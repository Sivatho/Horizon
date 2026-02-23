using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings
{
    public class CancelAdjustmentToBillingsValidationMethods : AbstractValidationMethods, ICancelAdjustmentToBillingsValidationMethods
    {
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
