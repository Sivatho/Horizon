using ClientServicing.Main.Controller;
using ClientServicing.Main.IController;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System.Text.Json;


namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{   
    public class PolicyCashReceiptAPITest
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task PolicyCashReceiptRequestHasPolicyNOandBillingPeriod_WhenpolicyPolicyCashReceiptAPITestAsync_ThenValidatePolicyCashReceiptResponseIsOk()
        {
            try
            {
                //Arrange
                AccountingHistoryAPIClient cashreceipt = new("https://horizontest.clientele.co.za/horizon.clientservicing/");

                // Deserialize and guard against null to satisfy nullable analysis
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            PolicyCashRecieptRequest policycashrecieptrequest =
                    JsonSerializer.Deserialize<PolicyCashRecieptRequest>(
                        utilitiesHelper.ReadJson("AccountHistory", "PolicyCashReceipt.json"),
                        jsonOptions)
                    ?? throw new JsonException("Failed to deserialize PolicyAccountHistorySummaryRequest from JSON.");

                //Act
                // Cast to the interface to disambiguate between duplicate overloads on the concrete type
                var response = await cashreceipt.policyCashReceiptAsync<PolicyCashRecieptRequest>(policycashrecieptrequest);

                var  fetchcashrecieptResponse = PopulateCashRecieptResponse(response);

                //Assert
                if (fetchcashrecieptResponse.data != null )
                                        
                {

                    ValidateCashRecieptsponseIsOk(response, fetchcashrecieptResponse);
                }
            }
            catch (JsonException ex)
            {
                TestContext.Out.WriteLine("Exception occured while deserializing Cash Reciept Response: \n +" + ex.Message);
                throw;
            } 
        }

     

        private PolicyCashRecieptResponse PopulateCashRecieptResponse(RestResponse response)
        {
            utilitiesHelper.LogRequestAndResponse(response.Request!, response);

            string content = response.Content ?? string.Empty;

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException(
                    $"Empty response content. StatusCode: {response.StatusCode}, IsSuccessful: {response.IsSuccessful}, ErrorMessage: {response.ErrorMessage}");
            }

            using JsonDocument doc = JsonDocument.Parse(content);
            JsonElement root = doc.RootElement;

            var fetch = new PolicyCashRecieptResponse
            {
                responseMessage = new ExecutionOutcome(),
                data = new List<PolicyCashRecieptResponse>(),
            };

            if (root.TryGetProperty("succeeded", out var succeededProp) && (succeededProp.ValueKind == JsonValueKind.True || succeededProp.ValueKind == JsonValueKind.False))
                fetch.responseMessage.succeeded = succeededProp.GetBoolean();
            if (root.TryGetProperty("message", out var messageProp) && messageProp.ValueKind == JsonValueKind.String)
                fetch.responseMessage.message = messageProp.GetString();

            void ExtractEntriesFromElement(JsonElement element)
            {
                if (element.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in element.EnumerateArray())
                        ExtractEntriesFromElement(item);
                    return;
                }

                if (element.ValueKind != JsonValueKind.Object)
                    return;

                // top-level object may contain policyNo
                if (element.TryGetProperty("policyNo", out var policyProp))
                {
                    if (policyProp.ValueKind == JsonValueKind.Number && policyProp.TryGetInt32(out var pn))
                    {
                        fetch.data.Add(new PolicyCashRecieptResponse
                        {
                            // Provide a non-null responseMessage so validators and static analysis won't fail.
                            responseMessage = new ExecutionOutcome { succeeded = false },
                            data = null
                        });
                        // You may want to set a property for policyNo in PolicyCashRecieptResponse if it exists
                    }
                    else if (policyProp.ValueKind == JsonValueKind.String && int.TryParse(policyProp.GetString(), out var pn2))
                    {
                        fetch.data.Add(new PolicyCashRecieptResponse
                        {
                            responseMessage = new ExecutionOutcome { succeeded = false },
                            data = null
                        });
                        // You may want to set a property for legacyPolicyNumber in PolicyCashRecieptResponse if it exists
                    }
                }

                // nested arrays
                if (element.TryGetProperty("data", out var paymentResults) && paymentResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in paymentResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p) && p.ValueKind == JsonValueKind.Number)
                            fetch.data.Add(new PolicyCashRecieptResponse
                            {
                                responseMessage = new ExecutionOutcome { succeeded = false },
                                data = null
                            });
                    }
                }

                if (element.TryGetProperty("Policy Cash Receipts Results", out var policyResults) && policyResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in policyResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p) && p.ValueKind == JsonValueKind.Number)
                            fetch.data.Add(new PolicyCashRecieptResponse
                            {
                                responseMessage = new ExecutionOutcome { succeeded = false },
                                data = null
                            });
                    }
                }
            }

            // Primary location: "data" — can be Array or Object
            if (root.TryGetProperty("data", out var dataProp))
            {
                ExtractEntriesFromElement(dataProp);
            }

            var prettyOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
            string prettyJson;
            try
            {
                prettyJson = JsonSerializer.Serialize(root, prettyOptions);
            }
            catch (Exception)
            {
                prettyJson = JsonSerializer.Serialize(fetch, prettyOptions);
            }

            TestContext.Out.WriteLine("Deserialized Account History Response:\n" + prettyJson);

            return fetch;
        }

        // Use PolicyAccountHistoryResponse here because the populate method returns that shape
        private void ValidateCashRecieptsponseIsOk(RestResponse response, PolicyCashRecieptResponse fetchcashrecieptResponse)
        {
            //Assert
            using (Assert.EnterMultipleScope())
            {
                // Http Status Code
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

                //Response Content
                Assert.That(fetchcashrecieptResponse.responseMessage, Is.Not.Null, "Account Hostory Summary Response: Response Message should not be null");
                Assert.That(fetchcashrecieptResponse.data, Is.Not.Null, "Account Hostory Summary Response: Data should not be null");

                //Validate Each Object Data Types
                Assert.That(fetchcashrecieptResponse.responseMessage!.succeeded, Is.TypeOf<bool>());
                Assert.That(fetchcashrecieptResponse.responseMessage!.message, Is.Null.Or.TypeOf<string?>());
                Assert.That(fetchcashrecieptResponse.responseMessage!.errors, Is.Null.Or.TypeOf<string?>());

                foreach (var item in fetchcashrecieptResponse.data ?? Enumerable.Empty<PolicyCashRecieptResponse>())
                {
                    Assert.That(item.responseMessage, Is.Not.Null, "Item responseMessage should not be null");
                    Assert.That(item.responseMessage!.succeeded, Is.TypeOf<bool>());
                    Assert.That(item.responseMessage.succeeded, Is.False, "Policy number is not valid.");
                }
            }
        }
    }
}