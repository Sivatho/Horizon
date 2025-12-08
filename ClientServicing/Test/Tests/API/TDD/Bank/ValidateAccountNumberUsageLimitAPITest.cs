using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateAccountNumberUsageLimitAPITest : ValidateAccountNumberUsageLimitValidationMethods
    {
        [Test]
        public async Task GivenAccountNumberUsageLimitIsValid_WhenValidateAccountNumberUsageLimitAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            string accountNumber = "1042631522";
            BankAPIClient bankClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            //Act
            var response = await bankClient.ValidateAccountNumberUsageLimitAsync(accountNumber);
            var validateAccountNumberUsageLimitResponse = populateValidateAccountNumberUsageLimitResponse(response);

            //Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Results:");
            ValidateHTTPResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(response);
            ValidateResponseIsNotNullOrEmpty(validateAccountNumberUsageLimitResponse);
        }
        private ValidateAccountNumberUsageLimitResponse populateValidateAccountNumberUsageLimitResponse(RestResponse response)
        {
            using JsonDocument document = JsonDocument.Parse(response.Content);
            ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse = new();
            foreach (var property in document.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":
                        validateAccountNumberUsageLimitResponse.success = property.Value.GetBoolean();
                        break;
                    case "message":
                        validateAccountNumberUsageLimitResponse.message = property.Value.GetString();
                        break;
                    case "totalPolicies":
                        validateAccountNumberUsageLimitResponse.totalPolicies = property.Value.GetInt32();
                        break;
                    case "limitExceeded":
                        validateAccountNumberUsageLimitResponse.limitExceeded = property.Value.GetBoolean();
                        break;
                }
            }
            return validateAccountNumberUsageLimitResponse;
        }

    }
}
