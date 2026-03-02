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
    public class CheckStatusAPITests : CheckStatusValidationMethods
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
        public async Task Given_CheckStatusRequestPayloadIsValid_When_ValidateCheckStatusResponseCodeOk_And_CheckStatusResponseIsOk_And_PropertyNameisValid_And_DataTypesIsValid_And_DataTypesIsValid_And_CheckStatusResponseDataIsNotNull()
        {
            //Arrange
            var checkStatusRequestData = new CheckStatusRequestData();
            var request = JsonSerializer.Deserialize<List<DebicheckRetryCheckStatusRequest>>(
                    utilitiesHelper.ReadTestDataJson
                    ("Debicheck\\Data", "CheckStatusRequestPayloadIsValid.json"))!;
            
            checkStatusRequestData.listOfCheckStatusRequest = request;
            ValidateCheckStatusRequestDataIsNotNullOrEmpty(checkStatusRequestData);

            //Act
            var response = await debicheckAPIClient!.CheckStatusAsync(request);
            var checkStatusResponse = PopulateCheckStatusResponse(response);
            var schema = ResponseSchemasEnvelope.checkStatusResponseSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateCheckStatusResponseDataIsNotNullOrEmpty(checkStatusResponse);
        }
    }
}