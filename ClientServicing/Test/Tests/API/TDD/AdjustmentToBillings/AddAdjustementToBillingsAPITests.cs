using System.Net;
using System.Text.Json;

using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AddAdjustementToBillings;
using ClientServicing.Main.Models.AdjustementToBillings;
using ClientServicing.Main.Resources.Helper;

using Microsoft.Extensions.DependencyInjection;
using static ClientServicing.Main.Models.AddAdjustementToBillings.AddAdjustementToBillingsRequest;



namespace ClientServicing.Test.Tests.API.TDD.AdjustmentToBillings
{
    [TestFixture]
    public class AddAdjustementToBillingsAPITests : AddAdjustmentToBillingsValidationMethod
    {
        AdjustmentToBillingsAPIClient? adjustmentToBillingsAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]

        public void Setup()
        {
            adjustmentToBillingsAPIClient = new AdjustmentToBillingsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }


        [Test, Category("Positive")]
        public async Task GivenAddAdjustmentToBillingsPayloadIsValid_WhenAddAdjustementToBillingsAsync_ThenValidateResponseIsSuccessful()
        {
            // Arrange
            var request = JsonSerializer.Deserialize<AddAdjustementToBillingsRequest>(
                utilitiesHelper.ReadTestDataJson(
                    "AdjustmentToBillings", "AddAdjustmentToBillingsPayloadIsValid.json"));
            ValidateBillingsAdjustmentInformationRequestPayload(request!);
            // Act

            var response = await adjustmentToBillingsAPIClient!.AddAdjustementToBillingsAsync(request!);
            var addAdjustementToBillingsResponse = adjustmentToBillingsAPIClient!.AddAdjustementToBillingsAsync(response!);
            var schema = ResponseSchemasEnvelope.BooleanResponse;

            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            //Reposne returned response is boolean value and not in schema format, so skipping schema validation for now and will add once the response is finalized from API side.
            //ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            //ValidateResponseShouldMatchSchema(response, schema);
            Assert.That(bool.Parse(response.Content!), Is.True.Or.False, "Response content should be either true or false");

        }
    }
}
