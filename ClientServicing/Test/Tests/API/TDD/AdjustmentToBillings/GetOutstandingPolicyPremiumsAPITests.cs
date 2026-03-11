
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
    [TestFixture]
    public class GetOutstandingPolicyPremiumsAPITests : GetOutstandingPolicyPremiumsValidationMethods
    {/// <summary>
    /// //
    /// </summary>
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
        public async Task GivenValidOutstandingPolicyPremiumRequest_WhenCallingAPI_ThenResponseIsValid()
        {
            // Arrange
            var json = utilitiesHelper.ReadTestDataJson(
                "AdjustmentToBillings",
                "GetOutstandingPolicyPremiumsPayloadIsValid.json"
            );

            var request = JsonSerializer.Deserialize<GetOutstandingPolicyPremiumsRequest>(json);

            // Validate request fields using inherited method
            ValidateGetOutstandingPolicyPremiumsRequestIsValid(request);

            // Act – Call API
            var restResponse = await adjustmentToBillingsAPIClient.GetOutstandingPolicyPremiumsAsync(request);

            // Assert raw response is not null
            Assert.That(restResponse, Is.Not.Null, "RestResponse should not be null.");

            // Deserialize API response
            var response = JsonSerializer.Deserialize<GetOutstandingPolicyPremiumsResponse>(
                restResponse.Content ?? string.Empty
            );

            // Validate response using inherited method
            ValidateGetOutstandingPolicyPremiumsResponseIsValid(response);
        }
    }
}
