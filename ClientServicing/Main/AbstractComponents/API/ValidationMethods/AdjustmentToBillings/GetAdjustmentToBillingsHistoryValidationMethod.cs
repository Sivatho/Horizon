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
    public class GetAdjustmentToBillingsHistoryValidationMethod : AbstractValidationMethods, IGetAdjustmentToBillingsHistoryValidationMethods
    {
        public void ValidateGetAdjustmentToBillingsHistoryRequestIsValid(GetAdjustmentToBillingsHistoryRequest request)
        {
            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null, "Request object should not be null.");
                Assert.That(request.policyNo > 0, Is.True, "policyNo should be greater than zero.");
                Assert.That(string.IsNullOrWhiteSpace(request.legacyPolicyNumber), Is.False, "legacyPolicyNumber should not be null or empty.");
                Assert.That(string.IsNullOrWhiteSpace(request.auditToken), Is.False, "auditToken should not be null or empty.");
            });
        }

        public void ValidateGetAdjustmentToBillingsHistoryResponseIsValid(GetAdjustmentToBillingsHistoryResponse response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null, "Response object should not be null.");
                Assert.That(response.succeeded, Is.True, "Response 'succeeded' should be true.");
                Assert.That(string.IsNullOrWhiteSpace(response.message), Is.False, "Response 'message' should not be null or empty.");

                Assert.That(response.data, Is.Not.Null, "Response 'data' should not be null.");
                Assert.That(response.data.Length > 0, Is.True, "Response 'data' array should not be empty.");

                foreach (var historyList in response.data)
                {
                    Assert.That(historyList, Is.Not.Null, "Each history list in 'data' should not be null.");
                    Assert.That(historyList.Count > 0, Is.True, "Each history list in 'data' should not be empty.");

                    foreach (var item in historyList)
                    {
                        ValidateGetAdjustmentToBillingsHistoryRequestIsValid(item);
                    }
                }
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
