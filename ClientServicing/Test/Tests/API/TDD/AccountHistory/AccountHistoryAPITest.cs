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
    public class AccountHistoryAPITest
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenAccountHistoryRequestHasPolicyNO_WhenpolicyAcountingHistoryAsync_ThenValidateAccountHistoryResponseIsOk()
        {
            try
            {
                //Arrange
                AccountingHistoryAPIClient accountHistoryAPIClient = new("https://horizontest.clientele.co.za/horizon.clientservicing/");
                PolicyAccountHistoryRequest policyAccountHistoryRequest = JsonSerializer.Deserialize<PolicyAccountHistoryRequest>(utilitiesHelper.ReadTestDataJson("AccountHistory", "PolicyAccountingHistoryRequestHasPolicy.json"));

                //Act
                var response = await accountHistoryAPIClient.policyAccountingHistoryAsync<PolicyAccountHistoryRequest>(policyAccountHistoryRequest);
                var fetchAccountHistoryResponse = populateFetchAccountHistoryResponse(response);

                //Assert
                if (fetchAccountHistoryResponse.data != null)
                {
                    ValidateAccountHistoryResponseIsOk(response, fetchAccountHistoryResponse);
                }
            }catch (JsonException ex)
            {
                TestContext.Out.WriteLine("Exception occured while deserializing Account History Response: \n +" + ex.Message);
                throw;
            }
        }
        // [Test]
        //public async Task GivenAccountHistoryRequestHasLegalRef_WhenpolicyAcountingHistoryAsync_ThenValidateAccountHistoryResponseIsOk
        //{

        //    AccountingHistoryAPIClient accountHistoryAPIClient = new("https://horizontest.clientele.co.za/horizon.clientservicing/");
        //    PolicyAccountHistoryRequest policyAccountHistoryRequest = JsonSerializer.Deserialize<PolicyAccountHistoryRequest>(utilitiesHelper.ReadJson("AccountHistory", "PolicyAccountingHistoryRequestHasLegalRef.json"));

        //    //Act
        //    var response = await accountHistoryAPIClient.policyAcountingHistoryAsync<PolicyAccountHistoryRequest>(policyAccountHistoryRequest.policyNo);
        //    var fetchAccountHistoryResponse = populateFetchAccountHistoryResponse(response);

        //    ValidateAccountHistoryResponseIsOk_andHasPolicyNoOrLegalRef(response, fetchAccountHistoryResponse);
        //}

        private PolicyAccountHistoryResponse populateFetchAccountHistoryResponse(RestResponse response)
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

            var fetch = new PolicyAccountHistoryResponse
            {
                responseMessage = new ExecutionOutcome(),
                data = new List<PolicyAccountHistoryRequest>(),
                accountingHistoryPaymentResults = new List<PolicyAccountHistoryRequest>(),
                accountingHistoryPolicyResults = new List<PolicyAccountHistoryRequest>()
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
                        fetch.data.Add(new PolicyAccountHistoryRequest { policyNo = pn });
                    }
                    else if (policyProp.ValueKind == JsonValueKind.String && int.TryParse(policyProp.GetString(), out var pn2))
                    {
                        fetch.data.Add(new PolicyAccountHistoryRequest { policyNo = pn2 });
                    }
                }

                // nested arrays
                if (element.TryGetProperty("accountingHistoryPaymentResults", out var paymentResults) && paymentResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in paymentResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p)     && p.ValueKind == JsonValueKind.Number)
                            fetch.accountingHistoryPaymentResults.Add(new PolicyAccountHistoryRequest { policyNo = p.GetInt32() });
                    }
                }

                if (element.TryGetProperty("accountingHistoryPolicyResults", out var policyResults) && policyResults.ValueKind == JsonValueKind.Array)
                {
                    foreach (var pr in policyResults.EnumerateArray())
                    {
                        if (pr.TryGetProperty("policyNo", out var p) && p.ValueKind == JsonValueKind.Number)
                            fetch.accountingHistoryPolicyResults.Add(new PolicyAccountHistoryRequest { policyNo = p.GetInt32() });
                    }
                }
            }

            // Primary location: "data" — can be Array or Object
            if (!root.TryGetProperty("data", out var dataProp ))
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

        private void ValidateAccountHistoryResponseIsOk(RestResponse response, PolicyAccountHistoryResponse fetchAccountHistoryResponse)
        {
            //Assert
            Assert.Multiple(() =>
            {
                // Http Status Code
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");

                //Response Content
                Assert.That(fetchAccountHistoryResponse.responseMessage, Is.Not.Null, "Fetch Banks Response: Response Message should not be null");
                Assert.That(fetchAccountHistoryResponse.data, Is.Not.Null, "Fetch Banks Response: Data should not be null");

                //Validate Each Object Data Types
                Assert.That(fetchAccountHistoryResponse.responseMessage.succeeded, Is.TypeOf<bool>());
                Assert.That(fetchAccountHistoryResponse.responseMessage.message, Is.Null.Or.TypeOf<string?>());
                Assert.That(fetchAccountHistoryResponse.responseMessage.errors, Is.Null.Or.TypeOf<string?>());
                foreach (var item in fetchAccountHistoryResponse.data)
                {
                    Assert.That(item.policyNo, Is.GreaterThan(0), "Policy number is not valid.");
                    
                }
            });
        }
    }
}

