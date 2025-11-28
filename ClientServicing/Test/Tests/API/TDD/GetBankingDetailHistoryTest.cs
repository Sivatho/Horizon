using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models;
using ClientServicing.Main.Models.Bank;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD
{ 
    public class GetBankingDetailHistoryTest
    {
        [Test]
        public async Task GivenPolicyNumberValid_WhenGetBankingDetailHistoryAsync_ThenValidateGetBankDetailHistoryResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            // Arrange
            int policyNumber = 164535;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);
            //Assert
            Then_ValidateGetBankDetailHistoryResponse_WhenResponseCodeIsOk_When_ExecutionOutcome_ANDDataIsNull_WhenPropertyDataTypesIsValid(response, getBankDetailHistoryResponse);

        }
        [Test]
        public async Task GivenPolicyNumberIsInvalid_WhenGetBankingDetailHistoryAsync_ThenValidateGetBankDetailHistoryResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            // Arrange
            int policyNumber = 0;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = populateGetBankDetailHistoryResponse(response);

            //Assert
            Then_ValidateGetBankDetailHistoryResponseWhen_ResponseCodeIsOk_And_ExecutionOutcomeIsNotNull_AndDataIsNull(response, getBankDetailHistoryResponse);
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

        private void Then_ValidateGetBankDetailHistoryResponse_WhenResponseCodeIsOk_When_ExecutionOutcome_ANDDataIsNull_WhenPropertyDataTypesIsValid(RestResponse response, GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            // Http Status Code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

            
            //Response Content
            Assert.That(getBankDetailHistoryResponse.executionOutcome, Is.Not.Null, "GetBank Detail History Response: Response Message should not be null");
            Assert.That(getBankDetailHistoryResponse.data, Is.Not.Null, "GetBank Detail History Response: Data should not be null");

            //Validate Each Object Data Types
            Assert.That(getBankDetailHistoryResponse.executionOutcome.succeeded, Is.TypeOf<bool>());
            Assert.That(getBankDetailHistoryResponse.executionOutcome.message, Is.Null.Or.TypeOf<string?>());
            Assert.That(getBankDetailHistoryResponse.executionOutcome.errors, Is.Null.Or.TypeOf<string?>());

            foreach (var getBankDetailHistory in getBankDetailHistoryResponse.data)
            {
                Assert.That(getBankDetailHistory.bankId, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.bankAccNo, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.bankName, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.bankAccHolder, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.bankAccTypeCd, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.bankAccTypeDescr, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.branchCode, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.debitDay, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.paymentMethod, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.bankAccountId, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.entityNo, Is.TypeOf<int>());
                Assert.That(getBankDetailHistory.effFrom, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.effTo, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.audModifyDate, Is.TypeOf<string>());
                Assert.That(getBankDetailHistory.audModifyUser, Is.TypeOf<string>());
            }
        }
        private void Then_ValidateGetBankDetailHistoryResponseWhen_ResponseCodeIsOk_And_ExecutionOutcomeIsNotNull_AndDataIsNull(RestResponse response, GetBankDetailHistoryResponse getBankDetailHistoryResponse)
        {
            // Http Status Code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

            //Response Content
            Assert.That(getBankDetailHistoryResponse.executionOutcome, Is.Not.Null, "GetBank Detail History Response: Execution Outcome should not be null");
            Assert.That(getBankDetailHistoryResponse.data, Is.Null.Or.Empty, "GetBank Detail History Response: Data should null");
        }
    }
}
