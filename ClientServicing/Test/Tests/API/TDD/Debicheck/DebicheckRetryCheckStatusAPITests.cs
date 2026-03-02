using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.Debicheck
{
    [TestFixture]
    public class DebicheckRetryCheckStatusAPITests : DebicheckRetryCheckStatusValidationMethods
    {
        DebicheckAPIClient? debicheckAPIClient = null!;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            debicheckAPIClient = new DebicheckAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        [Test]
        public async Task Given_DebicheckRetryCheckStatusPayloadIsValid_When_DebicheckRetryCheckStatusAsync_Then()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<DebicheckRetryCheckStatusRequest>(
                utilitiesHelper.ReadTestDataJson
                ("Debicheck\\Data", "DebicheckRetryCheckStatusPayloadIsValid.json"))!;
            ValidateDebicheckRetryCheckStatusRequestIsNotNullOrEmpty(request);
            //Act
            var response = await debicheckAPIClient!.DebicheckRetryCheckStatusAsync(request);
            var debicheckRetryCheckStatusResponse = PopulateDebicheckRetryCheckStatusResponse(response);
            var schema = ResponseSchemasEnvelope.DebicheckRetryCheckStatusSchema();
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateDebicheckRetryCheckStatusResponseIsNotNullOrEmpty(debicheckRetryCheckStatusResponse);
            
        }
    }
}
