using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings
{
    internal class GetOutstandingPolicyPremiumsValidationMethods : AbstractValidationMethods, IGetOutstandingPolicyPremiumsValidationMethods
    {
        public void ValidateGetOutstandingPolicyPremiumsRequestIsValid(GetOutstandingPolicyPremiumsRequest request)
        {
            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null, "Request should not be null.");
                Assert.That(request.policyNo, Is.GreaterThan(0), "PolicyNo should be greater than 0.");
                Assert.That(request.legacyPolicyNumber, Is.Not.Null.And.Not.Empty, "LegacyPolicyNumber should not be null or empty.");
                Assert.That(request.auditToken, Is.Not.Null.And.Not.Empty, "AuditToken should not be null or empty.");
            });
        }

        public void ValidateGetOutstandingPolicyPremiumsResponseIsValid(GetOutstandingPolicyPremiumsResponse response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null, "Response Should Not Be Null.");
                Assert.That(response.succeeded, Is.TypeOf<bool>(), "Success Should Be A Boolean.");
                Assert.That(response.message, Is.Null.Or.TypeOf<string>(), "Message Should Be Null Or A String.");
                Assert.That(response.errors, Is.Null.Or.TypeOf<string>(), "Errors Should Be Null Or A String.");
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
