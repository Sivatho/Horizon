using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.AdjustmentToBillings
{
    public class AddAdjustementToBillingsAPITests
    {
        UtilitiesHelper utilitiesHelper  = new();
        

        [Test]
        [Ignore("This test is ignored because it is not ready yet.")]
        public async Task GivenAdjustementToBillingsPayloadIsValid_WhenAddAdjustementToBillingsAsync_ThenValidateResponseIsSuccessful()
        {
            //Arrange
            AddAdjustementToBillingsRequest addAdjustementToBillingsRequest = JsonSerializer.Deserialize<AddAdjustementToBillingsRequest>(utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "AddAdjustementToBillingsPayloadIsValid.json"));
            AdjustmentToBillingsAPIClient adjustmentToBillingsAPIClient = new("https://horizontest.clientele.co.za/horizon.clientservicing/");
            addAdjustementToBillingsRequest.billingsAdjustmentInformation.effectiveDate = DateTime.Now;
            addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustmentDateFrom = DateTime.Now;
            addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustmentEndDate = DateTime.Now.AddMonths(1);
            BillingAdjustmentPeriods billingAdjustmentPeriods = new() {
                raisedDate = DateTime.Now,
                effectiveDate = DateTime.Now.AddMonths(1)
            };
            addAdjustementToBillingsRequest.billingAdjustmentPeriods.Add(billingAdjustmentPeriods);


            //Act
            var response = adjustmentToBillingsAPIClient.AddAdjustementToBillingsAsync(addAdjustementToBillingsRequest);
            
            //Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Result:");
        }
    }
}
