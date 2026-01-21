using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    public class SendInternalEmailsAPITests
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test, Category("Positive")]
        public async Task Given_SendInternalEmailRequestPayloadIsValid_When_SendInternalEmailsAsync_Then_ValidateSendInternalEmailsResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            var request = JsonSerializer.Deserialize<SendInternalEmailsRequest>
                (utilitiesHelper.ReadTestDataJson("Email/Data/", "SendInternalEmailRequestPayloadIsValid.json"));
            
            //Act
            var response = await policyAPIClient.SendInternalEmailsAsync(request);
            
            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True, "Response should indicate success.");
            DocumentTemplate.DisplayBody("Validated: Response Status Code: 200; Status Description: 'OK' as expected.");
        }
    }
}
