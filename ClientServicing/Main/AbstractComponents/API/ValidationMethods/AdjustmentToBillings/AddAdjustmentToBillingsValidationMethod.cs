using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Models.General;
using com.sun.org.apache.xpath.@internal.objects;
using com.sun.tools.corba.se.idl.constExpr;
using RestSharp;
using sun.invoke.empty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings
{
    public class AddAdjustmentToBillingsValidationMethod : AbstractValidationMethods, IAddAdjustmentToBillingsValidationMethods
	{

		public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
		{
			throw new NotImplementedException();
		}

		public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
		{
			throw new NotImplementedException();
		}
		
			public void ValidateAddAdjustementToBillingsRequestIsNotNullOrEmpty(AddAdjustementToBillingsRequest AddAdjustementToBillings)
		{
			Assert.Multiple(() =>
			{
				Assert.That(AddAdjustementToBillings, Is.Not.Null, "Request object should not be null.");
				// Add further field-level checks as needed, for example:
				// Assert.That(addAdjustementtobillingsrequest.SomeField, Is.Not.Null.Or.Empty, "SomeField should not be null or empty.");
			});
		}
		public void ValidateAddAdjustementToBillingsRequestIsValid(AddAdjustementToBillingsRequest AddAdjustementToBillings)
		{
			Assert.Multiple(() =>
			{
				Assert.That(AddAdjustementToBillings, Is.Not.Null, "Request object should not be null.");
				Assert.That(AddAdjustementToBillings.executionOutcome, Is.Not.Null, "executionOutcome should not be null.");
				Assert.That(AddAdjustementToBillings.executionOutcome.succeeded, Is.TypeOf<bool>(), "executionOutcome.succeeded should be a boolean.");
				Assert.That(AddAdjustementToBillings.executionOutcome.message, Is.Null.Or.TypeOf<string>(), "executionOutcome.message should be null or a string.");
				Assert.That(AddAdjustementToBillings.executionOutcome.errors, Is.Null.Or.TypeOf<string>(), "executionOutcome.errors should be null or a string.");

				Assert.That(AddAdjustementToBillings.data, Is.Not.Null, "data should not be null.");

				// Validate billingsadjustmentinformation
				Assert.That(AddAdjustementToBillings.data.billingsadjustmentinformation, Is.Not.Null, "billingsadjustmentinformation should not be null.");
				Assert.That(AddAdjustementToBillings.data.billingsadjustmentinformation.Length, Is.GreaterThan(0), "billingsadjustmentinformation should not be empty.");
				foreach (var info in AddAdjustementToBillings.data.billingsadjustmentinformation)
				{
					ValidateAddAdjustementToBillingsRequestIsValid(info);
				}

				// Validate billingadjustmentperiods
				Assert.That(AddAdjustementToBillings.data.billingadjustmentperiods, Is.Not.Null, "billingadjustmentperiods should not be null.");
				Assert.That(AddAdjustementToBillings.data.billingadjustmentperiods.Length, Is.GreaterThan(0), "billingadjustmentperiods should not be empty.");
				foreach (var period in AddAdjustementToBillings.data.billingadjustmentperiods)
				{
					ValidateAddAdjustementToBillingsRequestIsValid(period);
				}
			});
		}
		public void ValidateBillingsAdjustmentInformationRequestIsValid(AddAdjustementToBillingsRequest.BillingsAdjustmentInformationRequest info)
		{
			Assert.Multiple(() =>
			{
				Assert.That(info.policyNo, Is.GreaterThan(0), "policyNo should be greater than 0.");
				Assert.That(info.effectiveDate, Is.Not.EqualTo(default(DateTime)), "effectiveDate should be set.");
				Assert.That(info.adjustmentDateFrom, Is.Not.EqualTo(default(DateTime)), "adjustmentDateFrom should be set.");
				Assert.That(info.adjustmentAmount, Is.TypeOf<double>(), "adjustmentAmount should be a double.");
				Assert.That(info.totalAdjAmount, Is.TypeOf<double>(), "totalAdjAmount should be a double.");
				Assert.That(info.adjustedMonthCnt, Is.GreaterThanOrEqualTo(0), "adjustedMonthCnt should be non-negative.");
				Assert.That(info.adjustmentEndDate, Is.Not.EqualTo(default(DateTime)), "adjustmentEndDate should be set.");
				Assert.That(info.comment, Is.Not.Null, "comment should not be null.");
				Assert.That(info.actionID, Is.Not.Null.Or.Empty, "actionID should not be null or empty.");
			});
		}
		public void ValidateBillingAdjustmentPeriodsRequestIsValid(AddAdjustementToBillingsRequest.BillingAdjustmentPeriodsRequest period)
		{
			Assert.Multiple(() =>
			{
				Assert.That(period.policyNo, Is.GreaterThan(0), "policyNo should be greater than 0.");
				Assert.That(period.legacyPolNo, Is.Not.Null.Or.Empty, "legacyPolNo should not be null or empty.");
				Assert.That(period.referenceNO, Is.Not.Null.Or.Empty, "referenceNO should not be null or empty.");
				Assert.That(period.billingPeriod, Is.GreaterThan(0), "billingPeriod should be greater than 0.");
				Assert.That(period.raisedDate, Is.Not.EqualTo(default(DateTime)), "raisedDate should be set.");
				Assert.That(period.mandateType, Is.Not.Null.Or.Empty, "mandateType should not be null or empty.");
				Assert.That(period.paymentType, Is.Not.Null.Or.Empty, "paymentType should not be null or empty.");
				Assert.That(period.premiumAmount, Is.TypeOf<double>(), "premiumAmount should be a double.");
				Assert.That(period.amountPaid, Is.TypeOf<double>(), "amountPaid should be a double.");
				Assert.That(period.effectiveDate, Is.Not.EqualTo(default(DateTime)), "effectiveDate should be set.");
			});
		}

		private void ValidateAddAdjustementToBillingsRequestIsValid(AddAdjustementToBillingsRequest.BillingAdjustmentPeriodsRequest period)
        {
            throw new NotImplementedException();
        }

        private void ValidateAddAdjustementToBillingsRequestIsValid(AddAdjustementToBillingsRequest.BillingsAdjustmentInformationRequest info)
        {
            throw new NotImplementedException();
        }

        public void ValidateAddAdjustementToBillingsResponseIsTrue(object response)
		{
			Assert.Multiple(() =>
			{
				Assert.That(response, Is.Not.Null, "Response should not be null.");
				Assert.That(response, Is.TypeOf<bool>(), "Response should be of type bool.");
				Assert.That((bool)response, Is.True, "Response should be true.");
			});
		}

        public void ValidateAddAdjustmentToBillingsResponseDataIsNotNullOrEmpty(AddAdjustementToBillingsRequest addadjustementtobillings)
        {
            throw new NotImplementedException();
        }
    }
}
