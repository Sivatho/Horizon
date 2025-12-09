using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class CanChangeBankAccountAPITest
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenBankAccountId_WhenCanChangeBankAccountAsync_Then() {
            //Arrange
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            int bankAccountId = 20354;

            //Act
            var response = await bankAPIClient.CanChangeBankAccountAsync(bankAccountId);
            var canChangeBankAccountResponse = populateCanChangeBankAccountResponse(response);

            //Assert
        }
        [Test]
        public async Task GivenInvalidBankAccountId_WhenCanChangeBankAccountAsync_Then() {
            //Arrange
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            int bankAccountId = -1;

            //Act
            var response = await bankAPIClient.CanChangeBankAccountAsync(bankAccountId);
            var canChangeBankAccountResponse = populateCanChangeBankAccountResponse(response);

            //Assert
        }
        public CanChangeBankAccountResponse populateCanChangeBankAccountResponse(RestResponse response) {
            try {
                using JsonDocument document = JsonDocument.Parse(response.Content);

                CanChangeBankAccountResponse canChangeBankAccountResponse = new CanChangeBankAccountResponse
                {
                    data = new CompleteStatusMessages()
                };
                foreach(var property in document.RootElement.EnumerateObject()) {
                    switch(property.Name) {
                        case "succeeded":
                            canChangeBankAccountResponse.succeeded = property.Value.GetBoolean();
                            break;
                        case "message":
                            canChangeBankAccountResponse.message = property.Value.GetString();
                            break;
                        case "errors":
                            canChangeBankAccountResponse.errors = property.Value.GetString();
                            break;
                        case "data":
                            foreach(var dataProperty in property.Value.EnumerateObject()) {
                                switch(dataProperty.Name) {
                                    case "proCompleted":
                                        canChangeBankAccountResponse.data.proCompleted = dataProperty.Value.GetBoolean();
                                        break;
                                    case "success":
                                        canChangeBankAccountResponse.data.success = dataProperty.Value.GetBoolean();
                                        break;
                                    case "message":
                                        canChangeBankAccountResponse.data.message = dataProperty.Value.GetString();
                                        break;
                                }
                            }
                            break;
                        default:
                            TestContext.Out.WriteLine($"Unknown property in response: {property.Name}");
                            break;
                    }
                }
                return canChangeBankAccountResponse;
            }
            catch (Exception ex) {
                TestContext.Out.WriteLine($"\tCanChangeBankAccount > Exception occurred while deserializing response: {ex.Message}");
                TestContext.Out.WriteLine($"\tCanChangeBankAccount > Stack Trace: {ex.StackTrace}");
                return null;
            }
        }
    }
}
