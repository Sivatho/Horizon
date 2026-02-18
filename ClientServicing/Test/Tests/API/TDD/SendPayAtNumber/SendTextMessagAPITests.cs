using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.SendPayNumber;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Policy.DBModels;
using ClientServicing.Main.Models.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;
using ClientServicing.Main.Resources.Shared;
using com.sun.tools.javac.comp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ClientServicing.Test.Tests.API.TDD.SendPayAtNumber
{
    [TestFixture]
    public class SendTextMessagAPITests : SendTextMesageValidationMethods
    {

        private SendPayAtNumberAPIClient? sendPayAtNumberAPIClient = null;
        private UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        
        [SetUp]
        public void SetUp()
        {
            sendPayAtNumberAPIClient = new SendPayAtNumberAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }


        [Test]
        public async Task Given_When_SendTextMessageAsync_Then() {
            //Arrange
            var request = JsonSerializer.Deserialize<SendTextMessageRequest>(utilitiesHelper.ReadTestDataJson("SendPayAtNumber/Data", "SendTextMessageRequestPayloadIsValid.json"));
            ValidateSendTextMessageRequestIsNotNullOrEmpty(request);

            var expectedId = 123;
            var expectedValue = "ExpectedValue";

            //Act
            var response = await sendPayAtNumberAPIClient.SendTextMessageAsync(request);
            var schema = ResponseSchemasEnvelope.BooleanResponse;

            // Query database to validate API side effects or state
            var dbResults = await _dataAccess.QueryAsync<PolicyTable>("SELECT TOP (1) * FROM Polly_C.polmas.m_policy ");


            //Assert HTTPS
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response body should not be null or empty.");
            Assert.That(response.Content, Is.EqualTo("true").Or.EqualTo("false"), "Response body should be either 'true' or 'false'.");
            DocumentTemplate.DisplayBody($"Validated: response: '{response.Content}' is not null or empty and is either 'true' or 'false'.");
            //ValidateResponseShouldMatchSchema(response, schema);

            //Assert DB
            //var dbData = dbResults.FirstOrDefault();


        }
    }
}
