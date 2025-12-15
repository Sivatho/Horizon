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
        public async Task Given_AccountNumberUsageLimitIsValid_When_ValidateAccountNumberUsageLimitAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty()
        {
            //Arrange
            string accountNumber = "1042631522";
            BankAPIClient bankClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            //Act
            var response = await bankClient.ValidateAccountNumberUsageLimitAsync(accountNumber);
            var validateAccountNumberUsageLimitResponse = populateValidateAccountNumberUsageLimitResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
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
