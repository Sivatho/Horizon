using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;
using static ClientServicing.Main.Models.Debicheck.MandatesRequest;

namespace ClientServicing.Test.Tests.API.TDD.Debicheck
{
    [TestFixture]
    public class DetermineMandateTypeAPITests : DetermineMandateTypeValidationMethods
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
        public async Task Give_DetermineMandateTypeRequestPayloadIsValid_When_DetermineMandateTypeAsync_Then_ValidateResponseCodeOk_And_PropertyNameisValid_And_DataTypesIsValid_And_DataTypesIsValid_And_CheckStatusResponseDataIsNotNull() {
            //Arrange
            var determineMandateTypeRequestData = new DetermineMandateTypeRequestData();
            var request = JsonSerializer.Deserialize<List<DetermineMandateTypeRequest>>(
                    utilitiesHelper.ReadTestDataJson
                    ("Debicheck\\Data", "DetermineMandateTypeRequestPayloadIsValid.json"))!;

            determineMandateTypeRequestData.listOfDetermineMandateTypeRequest = request;
            ValidateDetermineMandateTypeRequestDataIsNotNullOrEmpty(determineMandateTypeRequestData);
            //Act
            var response = await debicheckAPIClient!.DetermineMandateTypeAsync(request);
            var determineMandateTypeResponse = PopulateDetermineMandateTypeResponse(response);
            var schema = ResponseSchemasEnvelope.DetermineMandateResponseResultSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateDetermineMandateTypeResponseDataIsNotNullOrEmpty(determineMandateTypeResponse);
        }

    }
}
