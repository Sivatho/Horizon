
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AdjustementToBillings;
using ClientServicing.Main.Resources.Helper;
using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AdjustmentToBillings
{
    [TestFixture]
    public class CancelAdjustmentToBillingsTests : CancelAdjustmentToBillingsValidationMethods
    {
        

         AdjustmentToBillingsAPIClient? adjustmentToBillingsAPIClient;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            adjustmentToBillingsAPIClient = new AdjustmentToBillingsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test, Category("Positive")]
        [Ignore("Stored Proc Not available to Cancel")]
        public async Task GivenCancelAdjustmentToBillingsPayloadIsValid_WhenCancelAdjustmentToBillingsAsync_ThenValidateResponseisSuccessful()
        {
            // Arrange
         
        

            var request = JsonSerializer.Deserialize<CancelAdjustmentToBillingsRequest>(utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "CancelAdjustmentToBillingsPayloadIsValid.json"));
            ValidatCancelAdjustmentToBillingsRequestDataIsNotNullOrEmpty(request);

            //Act
            var restResponse = await adjustmentToBillingsAPIClient.CancelAdjustmentToBillingsAsync(request);

            var response = JsonSerializer.Deserialize<CancelAdjustmentToBillingsResponse>(restResponse.Content ?? string.Empty);
            ValidateResponseStatusCode(restResponse, System.Net.HttpStatusCode.OK);
            ValidateResponseFieldParametersIsValid(response);
            


            // act


            // assert: basic HTTP



            // Optionally validate error shape here if your API returns a standard error envelope
        }
    }
}
