using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class GetBankingDetailHistoryAPITest : GetBankDetailsHisoryResponseValidationMethods
    {
        [Test]
        public async Task Given_PolicyNumberValid_When_GetBankingDetailHistoryAsync_Then_ValidateGetBankDetailHistoryResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            int policyNumber = 164535;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "GetBankDetailHistoryResponseSchema.json");
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);

        }
        [Test]
        public async Task Given_PolicyNumberIsInvalid_When_GetBankingDetailHistoryAsync_Then_ValidateGetBankDetailHistoryResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            int policyNumber = 0;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponseIsNotNullOrEmpty(getBankDetailHistoryResponse);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
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
