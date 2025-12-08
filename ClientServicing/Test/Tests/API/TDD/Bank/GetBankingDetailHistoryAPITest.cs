using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class GetBankingDetailHistoryAPITest : GetBankDetailsHisoryResponseValidationMethods
    {
        [Test]
        public async Task GivenPolicyNumberValid_WhenGetBankingDetailHistoryAsync_ThenValidateGetBankDetailHistoryResponseIsOk_AndContentsIsValid_And_DataTypesIsValid()
        {
            // Arrange
            int policyNumber = 164535;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);

            //Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Results:");
            ValidateHTTPResponseStatusCodeOK(response);
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);
            ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "GetBankDetailHistoryResponseSchema.json");
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);

        }
        [Test]
        public async Task GivenPolicyNumberIsInvalid_WhenGetBankingDetailHistoryAsync_ThenValidateGetBankDetailHistoryResponseIsOk_AndDataIsNotNullOrEmpty()
        {
            // Arrange
            int policyNumber = 0;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);

            //Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Results:");
            ValidateHTTPResponseStatusCodeOK(response);
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);
            ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "GetBankDetailHistoryResponseSchema.json");
            ValidateResponseIsNullOrWhiteSpace(getBankDetailHistoryResponse);
        }
        public GetBankDetailHistoryResponse populateGetBankDetailHistoryResponse(RestResponse response)
        {
            using JsonDocument doc = JsonDocument.Parse(response.Content);
            GetBankDetailHistoryResponse getBankDetailHistoryResponse = new GetBankDetailHistoryResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new List<GetBankDetailHistory>()
            };
            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":
                        getBankDetailHistoryResponse.executionOutcome.succeeded = property.Value.GetBoolean();
                        break;
                    case "message":
                        getBankDetailHistoryResponse.executionOutcome.message = property.Value.GetString();
                        break;
                    case "errors":
                        getBankDetailHistoryResponse.executionOutcome.errors = property.Value.GetString();
                        break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            GetBankDetailHistory bankDetailHistory = JsonSerializer.Deserialize<GetBankDetailHistory>(item.GetRawText());
                            getBankDetailHistoryResponse.data.Add(bankDetailHistory);
                        }
                        break;
                }
            }
            return getBankDetailHistoryResponse;
        }
    }
}
