using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Email;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Email
{
    public class SendInternalEmailsAPITest : SendInternalEmailsResponseValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        EmailAPIClient emailAPIClient = new EmailAPIClient();

        [Test]        
        public async Task Given_SendInternalEmailsRequestIsValid_When_SendInternalEmailsAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_SendInternalEmailsResponseDataIsNotNullOrEmpty()
        {
            //Arrange
            var sendInternalEmailsRequest = JsonSerializer.Deserialize<SendInternalEmailsRequest>(
                utilitiesHelper.ReadTestDataJson("Email/Data", "SendInternalEmailRequestPayloadIsValid.json"));

            //Act
            var response = await emailAPIClient.SendInternalEmailsAsync(sendInternalEmailsRequest);
            var sendInternalEmailsResponse = PopulateSendInternalEmailsResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateSendInternalEmailsResponseDataIsNotNullOrEmpty(sendInternalEmailsResponse);
        }
    }
}
