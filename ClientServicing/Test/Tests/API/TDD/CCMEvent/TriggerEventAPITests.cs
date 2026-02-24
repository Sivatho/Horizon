using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.CCMEvent;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.CCMEvent;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Testing.Platform.Services;

namespace ClientServicing.Test.Tests.API.TDD.CCMEvent
{
    [TestFixture]
    public class TriggerEventAPITests : TriggerEventValidationMethods
    {
        CCMEventAPIClient? ccmEventAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null;

        [SetUp]
        public void SetUp() {
            ccmEventAPIClient = new CCMEventAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test]
        public async Task Given_TriggerEventRequestPayloadIsValid_When_TriggerEventAsync_Then_ValidateTriggerEventResponseStatusIsOK() {
            //Arrange
            var request = JsonSerializer.Deserialize<TriggerEventRequest>(
                utilitiesHelper.ReadTestDataJson
                ("CCMEvent", "TriggerEventRequestPayloadIsValid.json"));
            ValidateTriggerEventRequestPayload(request);
            //Act
            var response = await ccmEventAPIClient.TriggerEventAsync(request);
            var triggerEventResponse = PopulateTriggerEventResponse(response);
            var schema = ResponseSchemasEnvelope.triggerEventSchema;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateTriggerEventResponsePayload(triggerEventResponse);
        }
    }
}
