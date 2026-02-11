using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.SendPayNumber;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.SendPayAtNumber
{
    [TestFixture]
    public class SendTextMessagAPITests : SendTextMesageValidationMethods
    {
        SendPayAtNumberAPIClient sendPayAtNumberAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_When_SendTextMessageAsync_Then() {
            //Arrange
            var request = JsonSerializer.Deserialize<SendTextMessageRequest>(utilitiesHelper.ReadTestDataJson("SendPayAtNumber/Data", "SendTextMessageRequestPayloadIsValid.json"));
            ValidateSendTextMessageRequestIsNotNullOrEmpty(request);

            //Act
            var response = await sendPayAtNumberAPIClient.SendTextMessageAsync(request);
            var schema = ResponseSchemasEnvelope.BooleanResponse;
            //var responseBody = response.Content;

            //JsonNode json = JsonNode.Parse(responseBody);
            //var reult = schema.Validate(json);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response body should not be null or empty.");
            Assert.That(response.Content, Is.EqualTo("true").Or.EqualTo("false"), "Response body should be either 'true' or 'false'.");
            //ValidateResponseShouldMatchSchema(response, schema);
        }
    }
}
