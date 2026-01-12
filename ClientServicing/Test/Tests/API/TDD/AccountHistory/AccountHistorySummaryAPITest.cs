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
    public class AccountHistorySummaryAPITest
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenAccountHistorySummaryRequestHasPolicyNOandBillingPeriod_WhenpolicyAcountingHistorySummaryAsync_ThenValidateAccountHistorySummaryResponseIsOk()
        {
            try
            {
                //Arrange
                AccountingHistoryAPIClient accountHistorySummaryAPIClient = new("https://horizontest.clientele.co.za/horizon.clientservicing/");

                // Deserialize and guard against null to satisfy nullable analysis
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            PolicyAccountHistorySummaryRequest policyAccountHistorySummaryRequest =
                    JsonSerializer.Deserialize<PolicyAccountHistorySummaryRequest>(
                        utilitiesHelper.ReadJson("AccountHistory", "PolicyAccountingHistorySummary.json"),
                        jsonOptions)
                    ?? throw new JsonException("Failed to deserialize PolicyAccountHistorySummaryRequest from JSON.");

                //Act
                // Cast to the interface to disambiguate between duplicate overloads on the concrete type
                var response = await accountHistorySummaryAPIClient.PolicyAccountingHistorySummaryAsync<PolicyAccountHistorySummaryRequest>(policyAccountHistorySummaryRequest);

                var  fetchaccounthistorysummarResponse = PopulateFetchAccountHistorysummaryResponse(response);

                //Assert
                if (fetchaccounthistorysummarResponse.data != null )
                                        
                {

                    ValidateAccountHistorySummaryResponseIsOk(response, fetchaccounthistorysummarResponse);
                }
            }
            catch (JsonException ex)
            {
                TestContext.Out.WriteLine("Exception occured while deserializing Account History Response: \n +" + ex.Message);
                throw;
            } 
        }

     

        private  PolicyAccountHistorySummaryResponse PopulateFetchAccountHistorysummaryResponse(RestResponse response)
        {
            // Log the request/response for debugging (UtilitiesHelper already has a helper)
            utilitiesHelper.LogRequestAndResponse(response.Request!, response);

            string content = response.Content ?? string.Empty;

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException(
                    $"Empty response content. StatusCode: {response.StatusCode}, IsSuccessful: {response.IsSuccessful}, ErrorMessage: {response.ErrorMessage}");
            }

            using JsonDocument doc = JsonDocument.Parse(content);
            JsonElement root = doc.RootElement;

            var fetch = new PolicyAccountHistorySummaryResponse
            {
                responseMessage = new ExecutionOutcome(),
                data = new List<PolicyAccountHistorySummaryRequest>(),
               
            };

            // read basic response message fields if present
            if (root.TryGetProperty("succeeded", out var succeededProp) && (succeededProp.ValueKind == JsonValueKind.True || succeededProp.ValueKind == JsonValueKind.False))
                fetch.responseMessage.succeeded = succeededProp.GetBoolean();
            if (root.TryGetProperty("message", out var messageProp) && messageProp.ValueKind == JsonValueKind.String)
                fetch.responseMessage.message = messageProp.GetString();

            // helper to extract policy entries from an element that can be object or array
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
                        fetch.data.Add(new PolicyAccountHistorySummaryRequest { PolicyNo = pn });
                    }
                    else if (policyProp.ValueKind == JsonValueKind.String && int.TryParse(policyProp.GetString(), out var pn2))
                    {
                        fetch.data.Add(new PolicyAccountHistorySummaryRequest { PolicyNo = pn2 });
                    }
                }

                // nested arrays
                if (element.TryGetProperty("data", out var paymentResults) && paymentResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in paymentResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p) && p.ValueKind == JsonValueKind.Number)
                            fetch.data.Add(new PolicyAccountHistorySummaryRequest { PolicyNo = p.GetInt32() });
                    }
                }

                if (element.TryGetProperty("accountingHistoryPolicyResults", out var policyResults) && policyResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in policyResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p) && p.ValueKind == JsonValueKind.Number)
                            fetch.data.Add(new PolicyAccountHistorySummaryRequest { PolicyNo = p.GetInt32() });
                    }
                }
            }

            // Primary location: "data" — can be Array or Object
            if (!root.TryGetProperty("data", out var dataProp))
            {
                ExtractEntriesFromElement(dataProp);
            }

            // Some responses may instead use the specific top-level names; handle them too defensively
            if (!root.TryGetProperty("accountingHistoryPaymentResults", out var Payment) && Payment.ValueKind == JsonValueKind.Array)
                ExtractEntriesFromElement(Payment);


            if (root.TryGetProperty("accountingHistoryPolicyResults", out var Policy) && Policy.ValueKind == JsonValueKind.Array)
                ExtractEntriesFromElement(Policy);

            // pretty-print the original JSON response (preserves all fields exactly as returned)
            var prettyOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
            string prettyJson;
            try
            {
                // use the parsed JsonDocument's root to produce nicely indented output
                prettyJson = JsonSerializer.Serialize(root, prettyOptions);
            }
            catch (Exception)
            {
                // fallback to serializing the constructed 'fetch' object
                prettyJson = JsonSerializer.Serialize(fetch, prettyOptions);
            }

            TestContext.Out.WriteLine("Deserialized Account History Response:\n" + prettyJson);

            return fetch;
        }

        // Use PolicyAccountHistoryResponse here because the populate method returns that shape
        private void ValidateAccountHistorySummaryResponseIsOk(RestResponse response, PolicyAccountHistorySummaryResponse fetchaccounthistorysummarResponse)
        {
            //Assert
            Assert.Multiple(() =>
            {
                // Http Status Code
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

                //Response Content
                Assert.That(fetchaccounthistorysummarResponse.responseMessage, Is.Not.Null, "Account Hostory Summary Response: Response Message should not be null");
                Assert.That(fetchaccounthistorysummarResponse.data, Is.Not.Null, "Account Hostory Summary Response: Data should not be null");

                //Validate Each Object Data Types
                Assert.That(fetchaccounthistorysummarResponse.responseMessage.succeeded, Is.TypeOf<bool>());
                Assert.That(fetchaccounthistorysummarResponse.responseMessage.message, Is.Null.Or.TypeOf<string?>());
                Assert.That(fetchaccounthistorysummarResponse.responseMessage.errors, Is.Null.Or.TypeOf<string?>());
                foreach (var item in fetchaccounthistorysummarResponse.data)
                {
                    Assert.That(item.PolicyNo, Is.GreaterThan(0), "Policy number is not valid.");

                }
            });
        }
    }
}