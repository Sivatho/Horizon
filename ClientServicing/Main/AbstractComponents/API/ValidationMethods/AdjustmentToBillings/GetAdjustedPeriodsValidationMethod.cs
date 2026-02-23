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
	public class GetAdjustedPeriodsValidationMethod : AbstractValidationMethods, IGetAdjustedPeriodsValidationMethods
	{

		public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
		{
			throw new NotImplementedException();
		}

		public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
		{
			throw new NotImplementedException();
		}

		public void ValidateAddAddAdjustementToBillingsRequestIsNotNullOrEmpty(BillingAdjustmentPeriodsRequest billingadjustmentperiods)
		{
			Assert.Multiple(() =>
			{
				Assert.That(billingadjustmentperiods, Is.Not.Null, "Request object should not be null.");
				// Add further field-level checks as needed, for example:
				// Assert.That(billingadjustmentperiods.SomeField, Is.Not.Null.Or.Empty, "SomeField should not be null or empty.");
			});
		}
		public void ValidateBillingAdjustmentPeriodsRequestIsValid(BillingAdjustmentPeriodsRequest request)
		
		{
			Assert.Multiple(() =>
			{
				Assert.That(request, Is.Not.Null, "BillingAdjustmentPeriodsRequest should not be null.");
				Assert.That(request.adjustmentID, Is.GreaterThan(0), "adjustmentID should be greater than 0.");
				Assert.That(request.policyNo, Is.GreaterThan(0), "policyNo should be greater than 0.");
				Assert.That(request.adjustmentDateFrom, Is.Not.EqualTo(default(DateTime)), "adjustmentDateFrom should be set.");
			});
		}
		}
		public void ValidateBillingAdjustmentPeriodsResponseIsTrue(object response)
		{
			Assert.Multiple(() =>
			{
				Assert.That(response, Is.Not.Null, "Response should not be null.");
				Assert.That(response, Is.TypeOf<bool>(), "Response should be of type bool.");
				Assert.That((bool)response, Is.True, "Response should be true.");
			});
		}

		public void ValidateAddAdjustmentToBillingsResponseDataIsNotNullOrEmpty(GetAdjustedPeriodsValidationMethod addadjustementtobillings)
		{
			throw new NotImplementedException();
		}
	}
}

        

