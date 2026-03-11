using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings;
using ClientServicing.Main.Models.AdjustementToBillings;
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
        public void ValidatCancelAdjustmentToBillingsRequestDataIsNotNullOrEmpty(CancelAdjustmentToBillingsRequest request)
        {
            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null, "Request should not be null.");
                Assert.That(request.policyNo, Is.GreaterThan(0), "PolicyNo should be greater than 0.");
                Assert.That(request.adjustmentID, Is.GreaterThan(0), "AdjustmentID should be greater than 0.");
            });
        }

        public  void ValidateResponseFieldParametersIsValid(CancelAdjustmentToBillingsResponse response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null, "Response should not be null.");
                Assert.That(response, Is.TypeOf<bool>(), "Response should be of type bool.");
            });
        }

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
