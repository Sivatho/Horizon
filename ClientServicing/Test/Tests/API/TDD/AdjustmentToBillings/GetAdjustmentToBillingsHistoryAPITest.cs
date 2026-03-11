
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Models.AdjustementToBillings;
using ClientServicing.Main.Resources.Helper;
using NUnit.Framework;
using System.Threading.Tasks;
using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AdjustmentToBillings
{
    public class GetAdjustmentToBillingsHistoryAPITest : GetAdjustmentToBillingsHistoryValidationMethod
    {
        AdjustmentToBillingsAPIClient? adjustmentToBillingsAPIClient;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void Setup()
        {
            adjustmentToBillingsAPIClient = new AdjustmentToBillingsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test, Category("Positive")]
        [Ignore("Data Displays Blank, Stored proc not ready")]
        public async Task GivenValidGetAdjustmentToBillingsHistoryRequest_WhenCallingAPI_ThenResponseIsValid()
        {
            // Arrange
            var json = utilitiesHelper.ReadTestDataJson(
                "AdjustmentToBillings",
                "GetAdjustmentToBillingsHistoryPayloadIsValid.json"
            );
            var request = JsonSerializer.Deserialize<GetAdjustmentToBillingsHistoryRequest>(json);
            // Validate request fields using inherited method
            ValidateGetAdjustmentToBillingsHistoryRequestIsValid(request);
            // Act – Call API
            var restResponse = await adjustmentToBillingsAPIClient.GetAdjustmentToBillingsHistoryAsync(request);
            // Assert raw response is not null
            Assert.That(restResponse, Is.Not.Null, "RestResponse should not be null.");
            // Deserialize API response
            var response = JsonSerializer.Deserialize<GetAdjustmentToBillingsHistoryResponse>(
                restResponse.Content ?? string.Empty
            );
            // Validate response using inherited method
            ValidateGetAdjustmentToBillingsHistoryResponseIsValid(response);
        }
    }
}
