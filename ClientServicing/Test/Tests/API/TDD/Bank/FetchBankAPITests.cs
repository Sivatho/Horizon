using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using NUnit.Framework.Constraints;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class FetchBankAPITests : FetchBankResponseValidationMethod
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            FetchBanksRequest fetchBankRequest = JsonSerializer.Deserialize<FetchBanksRequest>(utilitiesHelper.ReadJson("Bank", "FetchBanksRequestIsNotNull.json"));
            fetchBankRequest.lastChanged = DateTime.Now.AddDays(-10);

            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assertl
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            ValidateResponseContentsIsValid_AndDataTypesIsValid(response);
            ValidateResponseIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
        }
        [Test]
        public async Task GivenBankRequestIsNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            FetchBanksRequest fetchBankRequest = JsonSerializer.Deserialize<FetchBanksRequest>(utilitiesHelper.ReadJson("Bank", "FetchBanksRequestIsNull.json"));

            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);


            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            ValidateResponseContentsIsValid_AndDataTypesIsValid(response);
            ValidateResponseIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
        }

        private FetchBanksResponse populateFetchBanksResponse(RestResponse response)
        {
            using JsonDocument doc = JsonDocument.Parse(response.Content);

            FetchBanksResponse fetchBanksResponse = new FetchBanksResponse
            {
                responseMessage = new ExecutionOutcome(),
                data = new List<FetchBanksRequest>()
            };

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":
                        fetchBanksResponse.responseMessage.succeeded = property.Value.GetBoolean();
                        break;
                    case "message":
                        fetchBanksResponse.responseMessage.message = property.Value.GetString();
                        break;
                    case "errors":
                        fetchBanksResponse.responseMessage.errors = property.Value.GetString();
                        break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            var bank = new FetchBanksRequest
                            {
                                bankID = item.GetProperty("bankID").GetInt32(),
                                bankName = item.GetProperty("bankName").GetString(),
                                bankShortName = item.GetProperty("bankShortName").GetString(),
                                dispSeq = item.GetProperty("dispSeq").GetInt32(),
                                isActive = item.GetProperty("isActive").GetBoolean(),
                                lastChanged = item.GetProperty("lastChanged").GetDateTime(),
                                userID = item.GetProperty("userID").GetString()
                            };
                            fetchBanksResponse.data.Add(bank);
                        }
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unkown property: {property.Name}");
                        break;
                }
            }
            return fetchBanksResponse;
        }
    }
}
