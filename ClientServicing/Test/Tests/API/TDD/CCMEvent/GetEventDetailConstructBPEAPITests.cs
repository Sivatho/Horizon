using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.CCMEvent;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.CCMEvent;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.CCMEvent
{
    [TestFixture]
    public class GetEventDetailConstructBPEAPITests : GetEventDetailConstructBPEValidationMethods
    {
        CCMEventAPIClient? ccmEventAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null;

        [SetUp]
        public void SetUp()
        {
            ccmEventAPIClient = new CCMEventAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test]
        public async Task Given_GetEventDetailConstructBPRequestPayloadIsValid_When_GetEventDetailConstructBPAsync_Then_ValidateGetEventDetailConstructBPResponseStatusIsOK()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<GetEventDetailConstructBPERequest>(
                utilitiesHelper.ReadTestDataJson
                ("CCMEvent", "GetEventDetailConstructBPERequestPayloadIsValid.json"));
            ValidateGetEventDetailConstructBPERequestPayload(request);
            //Act
            var response = await ccmEventAPIClient.GetEventDetailConstructBPEAsync(request);
            var getEventDetailConstructBPEResponse = PopulateGetEventDetailConstructBPEResponse(response);
            var schema = ResponseSchemasEnvelope.getEventDetailConstructBPESchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateGetEventDetailConstructBPEResponsePayload(getEventDetailConstructBPEResponse);
        }
    }
}
