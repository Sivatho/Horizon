using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.CancelPolicy;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.CancelPolicy;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.CancelPolicy
{
    [TestFixture]
    public class UpdateCancelPolicyDetailsAPITests : UpdateCancelDetailsValidationMethods
    {
        CancelPolicyAPIClient? cancelPolicyAPIClient = null;
        UtilitiesHelper utilityHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            cancelPolicyAPIClient = new CancelPolicyAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        [Test]
        public async Task Given_UpdateCancelPolicyDetailsRequestIsValid_When_UpdateCancelPolicyDetailsAsync_Then_ValidateUpdateCancelPolicyDetailsResponseIsOk()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<UpdateCancelPolicyDetailsRequest>(
                utilityHelper.ReadTestDataJson
                ("CancelPolicy", "UpdateCancelPolicyDetailsRequest.json"));
            //Act
            var response = await cancelPolicyAPIClient!.UpdateCancelPolicyDetailsAsync<UpdateCancelPolicyDetailsRequest>(request);
            var updateCancelPolicyDetailsResponse = PopulateUpdateCancelDetailsResponse(response);
            var schema = ResponseSchemas.StandardResponseDataBoolSchema();
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateUpdateCancelDetailsResponseIsNotNullOrEmpty(updateCancelPolicyDetailsResponse);
        }
    }
}
