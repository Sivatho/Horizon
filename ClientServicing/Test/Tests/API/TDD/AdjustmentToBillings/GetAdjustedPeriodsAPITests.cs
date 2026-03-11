
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
    
    public class GetAdjustedPeriodsAPITests : GetAdjustedPeriodsValidationMethod
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
        [Ignore("Stored Procedure not implemented")]
        public async Task GivenGetAdjustedPeriodsAPITestsPayloadIsValid_WhenCancelAdjustmentToBillingsAsync_ThenValidateResponseisSuccessful()
        {
            // Arrange
            var json = utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "GetAdjustedPeriodsPayloadIsValid.json");
            
            var request = JsonSerializer.Deserialize<GetAdjustedPeriodsRequest>(utilitiesHelper.ReadTestDataJson("AdjustmentToBillings", "GetAdjustedPeriodsPayloadIsValid.json"));
            ValidatGetAdjustedPeriodsRequestDataIsNotNullOrEmpty(request!);

            //Act
            var restResponse = await adjustmentToBillingsAPIClient!.GetAdjustedPeriodsAsync(request!);

            var response = JsonSerializer.Deserialize<BillingAdjustmentPeriodsResponse>(restResponse.Content ?? string.Empty);
            ValidateResponseStatusCode(restResponse, System.Net.HttpStatusCode.OK);
            ValidateResponseFieldParametersIsValid(response);
             // act

            // assert: basic HTTP

            // Optionally validate error shape here if your API returns a standard error envelope
        }

        private void ValidateResponseFieldParametersIsValid(BillingAdjustmentPeriodsResponse? response)
        {
            throw new NotImplementedException();
        }
    }
}
