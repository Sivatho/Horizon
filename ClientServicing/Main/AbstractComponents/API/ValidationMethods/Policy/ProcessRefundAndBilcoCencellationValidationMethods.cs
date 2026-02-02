using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class ProcessRefundAndBilcoCencellationValidationMethods : AbstractValidationMethods, IProcessRefundAndBilcoCencellationValidationMethods
    {
        public void ValidateProcessRefundAndBilcoCencellationRequestPayload(ProcessRefundAndBilcoCancellationRequest processRefundAndBilcoCencellationRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(processRefundAndBilcoCencellationRequest.billingSubmitted,  Is.Not.LessThan(0), "BillingSubmitted Should Not Be Less Than Zore");
                Assert.That(processRefundAndBilcoCencellationRequest.changeType,        Is.Not.LessThan(0), "ChangeType Should Not Be Less Than Zore");
                Assert.That(processRefundAndBilcoCencellationRequest.refundAvail,       Is.Not.LessThan(0), "RefundAvail Should Not Be Less Than Zore");
                Assert.That(processRefundAndBilcoCencellationRequest.policyNo,          Is.Not.LessThan(0), "PolicyNo Should Not Be Less Than Zore");
                Assert.That(processRefundAndBilcoCencellationRequest.refundStatus,      Is.Not.LessThan(0), "Refund Status Should Not Be Less Than Zore");
                Assert.That(processRefundAndBilcoCencellationRequest.refundAmount,      Is.Not.LessThan(0), "Should Not Be Less Than Zore");
            }
            DocumentTemplate.DisplayBody("Validated: ProcessRefundAndBilcoCancellationRequest Data Integers Is Not Less Than Zero");
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
