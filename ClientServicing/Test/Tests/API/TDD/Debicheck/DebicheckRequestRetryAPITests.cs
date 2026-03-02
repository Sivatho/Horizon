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

namespace ClientServicing.Test.Tests.API.TDD.Debicheck
{
    public class DebicheckRequestRetryAPITests : DebitcheckRequestRetryValidationMethods
    {
        DebicheckAPIClient? debicheckAPIClient = null;
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
            var response = await debicheckAPIClient!.DebicheckRequestRetryAsync(request);
            var debicheckRetryCheckStatusResponse = PopulateDebicheckRetryCheckStatusResponseData(response);
            var schema = ResponseSchemasEnvelope.DebitcheckRequestRetry();
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
