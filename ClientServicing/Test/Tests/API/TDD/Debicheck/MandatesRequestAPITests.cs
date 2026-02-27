using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
    public class MandatesRequestAPITests : MandateRequestValidationMethods
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
        public async Task Given_MandatesRequestPayloadIsValid_When_MandatesRequestAsync_Then_ValidateResponseCodeOk_And_PropertyNameisValid_And_DataTypesIsValid_And_DataTypesIsValid_And_CheckStatusResponseDataIsNotNull()
        {
            //Arrange
            var mandatesRequestData = new MandatesRequestData();
            var request = JsonSerializer.Deserialize<List<MandatesRequest>>(
                    utilitiesHelper.ReadTestDataJson
                    ("Debicheck\\Data", "MandatesRequestPayloadIsValid.json"))!;

            mandatesRequestData.listOfMandatesRequest = request;
            ValidateMandateRequesDataIsNotNullOrEmpty(mandatesRequestData);
            //Act
            var response = await debicheckAPIClient!.MandatesRequestAsync(request);
            var mandatesRequestResponse = PopulateMandatesRequestResponse(response);
            var schema = ResponseSchemasEnvelope.MandatesRequestResponseSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateMandateResponseDataIsNotNullOrEmpty(mandatesRequestResponse);
        }
    }
}
