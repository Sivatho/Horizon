using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Models.AdjustementToBillings;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.AdjustmentToBillings
{
    public class AddAdjustementToBillingsAPITests : AddAdjustmentToBillingsValidationMethod
    {
        AdjustmentToBillingsAPIClient? addadjustmentTOBillingsAPIClient ;
        UtilitiesHelper utilitiesHelper = new();

        [Test, Category("Positive")]
        
        public async Task GivenAdjustementToBillingsPayloadIsValid_WhenAddAdjustementToBillingsAsync_ThenValidateResponseIsSuccessful()
        {
            // Arrange
            var json = utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "AddAdjustementToBillingsPayloadIsValid.json");
            AddAdjustementToBillingsRequest addAdjustementToBillingsRequest = JsonSerializer.Deserialize<AddAdjustementToBillingsRequest>(json);

            // Set dates for billingsadjustmentinformation
            if (addAdjustementToBillingsRequest?.data?.billingsadjustmentinformation != null)
            {
                foreach (var info in addAdjustementToBillingsRequest.data.billingsadjustmentinformation)
                {
                    info.effectiveDate = DateTime.Now;
                    info.adjustmentDateFrom = DateTime.Now;
                    info.adjustmentEndDate = DateTime.Now.AddMonths(1);
                }
            }

            // Set dates for billingadjustmentperiods
            if (addAdjustementToBillingsRequest?.data?.billingadjustmentperiods != null)
            {
                foreach (var period in addAdjustementToBillingsRequest.data.billingadjustmentperiods)
                {
                    period.raisedDate = DateTime.Now;
                    period.effectiveDate = DateTime.Now.AddMonths(1);
                }
            }

            // Validate request
            ValidateAddAdjustementToBillingsRequestIsValid(addAdjustementToBillingsRequest);

            // Fix: Pass an instance of IRestLibrary instead of a string
            IRestLibrary restLibrary = new RestLibrary(); // Assuming RestLibrary implements IRestLibrary
            AdjustmentToBillingsAPIClient adjustmentToBillingsAPIClient = new(restLibrary);

            // Act
            var response = await adjustmentToBillingsAPIClient.AddAdjustementToBillingsAsync(addAdjustementToBillingsRequest);

            // Validate response
            ValidateAddAdjustementToBillingsResponseIsTrue(response);

            // Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Result:");
        }
		[Test, Category("Positive")]
		public async Task GivenOutstandingPolicyPremiumsPayloadIsValid_WhenGetOutstandingPolicyPremiumsAsync_ThenValidateResponseIsSuccessful()
		{
			// Arrange
			var json = utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "GetOutstandingPolicyPremiumsPayloadIsValid.json");
			AddAdjustementToBillingsRequest getOutstandingPolicyPremiumsRequest = JsonSerializer.Deserialize<AddAdjustementToBillingsRequest>(json);

			// Set dates for billingsadjustmentinformation
			if (getOutstandingPolicyPremiumsRequest?.data?.billingsadjustmentinformation != null)
			{
				foreach (var info in getOutstandingPolicyPremiumsRequest.data.billingsadjustmentinformation)
				{
					info.effectiveDate = DateTime.Now;
					info.adjustmentDateFrom = DateTime.Now;
					info.adjustmentEndDate = DateTime.Now.AddMonths(1);
				}
			}

			// Set dates for billingadjustmentperiods
			if (getOutstandingPolicyPremiumsRequest?.data?.billingadjustmentperiods != null)
			{
				foreach (var period in getOutstandingPolicyPremiumsRequest.data.billingadjustmentperiods)
				{
					period.raisedDate = DateTime.Now;
					period.effectiveDate = DateTime.Now.AddMonths(1);
				}
			}

			// Validate request
			ValidateAddAdjustementToBillingsRequestIsValid(getOutstandingPolicyPremiumsRequest);

			// Pass an instance of IRestLibrary
			IRestLibrary restLibrary = new RestLibrary();
			AdjustmentToBillingsAPIClient adjustmentToBillingsAPIClient = new(restLibrary);

			// Act
			var response = await adjustmentToBillingsAPIClient.GetOutstandingPolicyPremiumsAsync(getOutstandingPolicyPremiumsRequest);

			// Validate response
			ValidateAddAdjustementToBillingsResponseIsTrue(response);

			// Assert
			TestContext.Out.WriteLine("\n======================================================================\nAssertion Result:");
		}
		[Test, Category("Positive")]
		public async Task GivenAdjustedPeriodsPayloadIsValid_WhenGetAdjustedPeriodsAsync_ThenValidateResponseIsSuccessful()
		{
			// Arrange
			var json = utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "GetAdjustedPeriodsPayloadIsValid.json");
			AddAdjustementToBillingsRequest getAdjustedPeriodsRequest = JsonSerializer.Deserialize<AddAdjustementToBillingsRequest>(json);

			// Set dates for billingsadjustmentinformation
			if (getAdjustedPeriodsRequest?.data?.billingsadjustmentinformation != null)
			{
				foreach (var info in getAdjustedPeriodsRequest.data.billingsadjustmentinformation)
				{
					info.effectiveDate = DateTime.Now;
					info.adjustmentDateFrom = DateTime.Now;
					info.adjustmentEndDate = DateTime.Now.AddMonths(1);
				}
			}

			// Set dates for billingadjustmentperiods
			if (getAdjustedPeriodsRequest?.data?.billingadjustmentperiods != null)
			{
				foreach (var period in getAdjustedPeriodsRequest.data.billingadjustmentperiods)
				{
					period.raisedDate = DateTime.Now;
					period.effectiveDate = DateTime.Now.AddMonths(1);
				}
			}

			// Validate request
			ValidateAddAdjustementToBillingsRequestIsValid(getAdjustedPeriodsRequest);

			// Pass an instance of IRestLibrary
			IRestLibrary restLibrary = new RestLibrary();
			AdjustmentToBillingsAPIClient adjustmentToBillingsAPIClient = new(restLibrary);

			// Act
			var response = await adjustmentToBillingsAPIClient.GetAdjustedPeriodsAsync(getAdjustedPeriodsRequest);

			// Validate response
			ValidateAddAdjustementToBillingsResponseIsTrue(response);

			// Assert
			TestContext.Out.WriteLine("\n======================================================================\nAssertion Result:");
		}

        [Test, Category("Positive")]
        public async Task GivenAdjustmentToBillingsHistoryPayloadIsValid_WhenGetAdjustmentToBillingsHistoryAsync_ThenValidateResponseIsSuccessful()
        {
            // Arrange
            var json = utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "GetAdjustmentToBillingsHistoryPayloadIsValid.json");
            GetAdjustmentToBillingsHistoryRequest getAdjustmentToBillingsHistoryRequest = JsonSerializer.Deserialize<GetAdjustmentToBillingsHistoryRequest>(json);

            // Validate request
            ValidateGetAdjustmentToBillingsHistoryRequestIsValid(getAdjustmentToBillingsHistoryRequest);

            // Pass an instance of IRestLibrary
            IRestLibrary restLibrary = new RestLibrary();
            AdjustmentToBillingsAPIClient adjustmentToBillingsAPIClient = new(restLibrary);

            // Act
            var response = await adjustmentToBillingsAPIClient.GetAdjustmentToBillingsHistoryAsync(getAdjustmentToBillingsHistoryRequest);

            // Deserialize response to strongly typed object
            var responseContent = response.Content;
            GetAdjustmentToBillingsHistoryResponse getAdjustmentToBillingsHistoryResponse = JsonSerializer.Deserialize<GetAdjustmentToBillingsHistoryResponse>(responseContent);

            // Validate response
            ValidateGetAdjustmentToBillingsHistoryResponseIsValid(getAdjustmentToBillingsHistoryResponse);

            // Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Result:");
        }

        private void ValidateGetAdjustmentToBillingsHistoryResponseIsValid(GetAdjustmentToBillingsHistoryResponse? getAdjustmentToBillingsHistoryResponse)
        {
            throw new NotImplementedException();
        }

        private void ValidateGetAdjustmentToBillingsHistoryRequestIsValid(GetAdjustmentToBillingsHistoryRequest? getAdjustmentToBillingsHistoryRequest)
        {
            throw new NotImplementedException();
        }
    }
}