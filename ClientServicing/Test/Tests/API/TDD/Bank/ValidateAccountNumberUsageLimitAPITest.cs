using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateAccountNumberUsageLimitAPITest
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
            Then_ValidateValidateAccountNumberUsageLimitResponseIsOk_AndIsNotNull_AndDataTypesIsValid(response, validateAccountNumberUsageLimitResponse);
        }
        private ValidateAccountNumberUsageLimitResponse populateValidateAccountNumberUsageLimitResponse(RestResponse response) { 
            using JsonDocument document = JsonDocument.Parse(response.Content);
            ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse = new ValidateAccountNumberUsageLimitResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in document.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":
                        validateAccountNumberUsageLimitResponse.executionOutcome.succeeded = property.Value.GetBoolean();
                        break;
                    case "message":
                        validateAccountNumberUsageLimitResponse.executionOutcome.message = property.Value.GetString();
                        break;
                    case "errors":
                        validateAccountNumberUsageLimitResponse.executionOutcome.errors = property.Value.GetString();
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

        public void Then_ValidateValidateAccountNumberUsageLimitResponseIsOk_AndIsNotNull_AndDataTypesIsValid(RestResponse response, ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse)
        {
            // Http Status Code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            //Response Content
            Assert.That(validateAccountNumberUsageLimitResponse.executionOutcome, Is.Not.Null, "Validate Account Number Usage Limit Response: Execution Outcome should not be null");
            //Validate Each Object Data Types
            Assert.That(validateAccountNumberUsageLimitResponse.executionOutcome.succeeded, Is.TypeOf<bool>());
            Assert.That(validateAccountNumberUsageLimitResponse.executionOutcome.message, Is.Null.Or.TypeOf<string?>());
            Assert.That(validateAccountNumberUsageLimitResponse.executionOutcome.errors, Is.Null.Or.TypeOf<string?>());
            Assert.That(validateAccountNumberUsageLimitResponse.totalPolicies, Is.TypeOf<int>());
            Assert.That(validateAccountNumberUsageLimitResponse.limitExceeded, Is.TypeOf<bool>());
        }
    }
}
