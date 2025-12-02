using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD
{
    public class FetchBankAPITests
    {       
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid() {
            //Arrange
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            FetchBanksRequest fetchBankRequest = JsonSerializer.Deserialize<FetchBanksRequest>(utilitiesHelper.ReadJson("Bank", "FetchBanksRequestIsNotNull.json"));
            fetchBankRequest.lastChanged = DateTime.Now.AddDays(-10);

            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assert
            ValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid(response, fetchBanksResponse);
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
            ValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid(response, fetchBanksResponse);
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
        private void ValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid(RestResponse response, FetchBanksResponse fetchBanksResponse)
        {
            // Http Status Code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

            //Response Content
            Assert.That(fetchBanksResponse.responseMessage, Is.Not.Null, "Fetch Banks Response: Response Message should not be null");
            Assert.That(fetchBanksResponse.data, Is.Not.Null, "Fetch Banks Response: Data should not be null");

            //Validate Each Object Data Types
            Assert.That(fetchBanksResponse.responseMessage.succeeded, Is.TypeOf<bool>());
            Assert.That(fetchBanksResponse.responseMessage.message, Is.Null.Or.TypeOf<string?>());
            Assert.That(fetchBanksResponse.responseMessage.errors, Is.Null.Or.TypeOf<string?>());

            foreach (var bank in fetchBanksResponse.data)
            {
                Assert.That(bank.bankID, Is.TypeOf<int>());
                Assert.That(bank.bankName, Is.TypeOf<string>());
                Assert.That(bank.bankShortName, Is.Null.Or.TypeOf<string>());
                Assert.That(bank.dispSeq, Is.TypeOf<int>());
                Assert.That(bank.isActive, Is.TypeOf<bool>());
                Assert.That(bank.bankID, Is.TypeOf<int>());
                Assert.That(bank.lastChanged, Is.TypeOf<DateTime>());
                Assert.That(bank.userID, Is.TypeOf<string>());
            }
        }

    }
}
