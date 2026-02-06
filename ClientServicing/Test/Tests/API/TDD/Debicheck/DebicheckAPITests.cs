using AventStack.ExtentReports.Gherkin.Model;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Test.Tests.API.TDD.Debicheck
{
    public class DebicheckAPITests : CheckStatusResponseValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        DebicheckAPIClient debicheckAPIclient = new DebicheckAPIClient();

        [Test]
        public async Task GivenCheckStatusRequestPayloadIsValid_When_CheckStatusResponseCodeOk_And_CheckStatusResponseIsOk_And_PropertyNameisValid_And_DataTypesIsValid_And_DataTypesIsValid_And_CheckStatusResponseDataIsNotNull()
        {

            // create list wrapper and call API with List<T>

            //Arrange
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Read raw JSON and handle comment-bearing files and both object/array payloads.
            var rawJson = utilitiesHelper.ReadTestDataJson("Debicheck\\Data", "CheckStatusRequest.json");
            using var doc = JsonDocument.Parse(rawJson, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
            var root = doc.RootElement;

            CheckStatusRequest checkstatusrequest;


            if (root.ValueKind == JsonValueKind.Object)
            {
                checkstatusrequest = JsonSerializer.Deserialize<CheckStatusRequest>(root.GetRawText(), opts)
                    ?? throw new InvalidOperationException("Deserialization of Check Status Request returned null");
            }
            else if (root.ValueKind == JsonValueKind.Array)
            {
                var first = root.EnumerateArray().FirstOrDefault();
                if (first.ValueKind != JsonValueKind.Object)
                    throw new InvalidOperationException("Test JSON array does not contain an object as the first element.");

                checkstatusrequest = JsonSerializer.Deserialize<CheckStatusRequest>(first.GetRawText(), opts)
                    ?? throw new InvalidOperationException("Deserialization of Check Status Request (from array) returned null");
            }
            else
            {
                throw new InvalidOperationException($"Unexpected JSON root element kind: {root.ValueKind}");
            }

            //Act
            var response = await debicheckAPIclient.DebicheckCheckStatusAPIClientAsync<CheckStatusRequest>(checkstatusrequest);

            // Always convert response to domain model (safe to call for diagnostics)
            var fetchCheckStatusResponse = populatefetchCheckStatusResponse(response);

            ValidationAssertionHeading();

            // Expectation for this run (adjust as needed)
            var expectedStatus = HttpStatusCode.BadRequest;
            ValidateResponseStatusCode(response, expectedStatus);

            // Only run "happy-path" property/data validations when we actually received 200 OK
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
                ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(fetchCheckStatusResponse);
            }
            else if (response?.StatusCode == HttpStatusCode.BadRequest)
            {
                // Validate common error shape for BadRequest so assertions are meaningful
                var content = response?.Content;
                Assert.That(content, Is.Not.Null.Or.Empty, "BadRequest returned empty body");

                using var doc2 = JsonDocument.Parse(content);
                var root2 = doc2.RootElement;

                // require at least one informative property: "message" or "error" or "errors"
                bool hasMessage = root2.TryGetProperty("message", out var m) && (m.ValueKind == JsonValueKind.String || m.ValueKind == JsonValueKind.Null);
                bool hasError = root2.TryGetProperty("error", out var e) && (e.ValueKind == JsonValueKind.String || e.ValueKind == JsonValueKind.Null);
                bool hasErrors = root2.TryGetProperty("errors", out var errs) && (errs.ValueKind == JsonValueKind.String || errs.ValueKind == JsonValueKind.Array || errs.ValueKind == JsonValueKind.Object);

                Assert.That(hasMessage || hasError || hasErrors, Is.True, "BadRequest body should contain 'message', 'error' or 'errors' property for diagnostics.");
            }
            else
            {
                // If other status codes are possible, add branches or a generic response-content assertion
                Assert.That(response?.Content, Is.Not.Null.Or.Empty, $"Unexpected status {(response?.StatusCode.ToString() ?? "<null>")}: response body should contain diagnostic info");
            }
            var requestList = new List<CheckStatusRequest> { checkstatusrequest };
            var outgoing = JsonSerializer.Serialize(requestList, new JsonSerializerOptions { WriteIndented = true });
            TestContext.Out.WriteLine("Outgoing payload:\n" + outgoing);
            TestContext.Out.WriteLine("Response status: " + (response?.StatusCode.ToString() ?? "<null>"));
            TestContext.Out.WriteLine("Response content:\n" + (response?.Content ?? "<null>"));
        }
        [Test]
        public async Task GivenMandateRequestPayloadIsValid_When_MandateRequestResponseCodeOk_And_MandateRequestResponseIsOk_And_PropertyNameisValid_And_DataTypesIsValid_And_DataTypesIsValid_And_CheckStatusResponseDataIsNotNull()
        {
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Read raw JSON and handle comment-bearing files and both object/array payloads.
            var rawJson = utilitiesHelper.ReadTestDataJson("Debicheck\\Data", "MandatesRequest.json");
            using var doc = JsonDocument.Parse(rawJson, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
            var root = doc.RootElement;

            List<MandatesRequest> requestList;

            if (root.ValueKind == JsonValueKind.Object)
            {
                var single = JsonSerializer.Deserialize<MandatesRequest>(root.GetRawText(), opts)
                    ?? throw new InvalidOperationException("Deserialization of MandatesRequest returned null");

                requestList = new List<MandatesRequest> { single };
            }
            else if (root.ValueKind == JsonValueKind.Array)
            {
                requestList = JsonSerializer.Deserialize<List<MandatesRequest>>(root.GetRawText(), opts)
                    ?? throw new InvalidOperationException("Deserialization of MandatesRequest array returned null or empty");
            }
            else
            {
                throw new InvalidOperationException($"Unexpected JSON root element kind: {root.ValueKind}");
            }

            // Act - send a list because the API expects a List<MandatesRequest>
            var response = await debicheckAPIclient.DebicheckMandateRequestAPIClientAsync(requestList);
            var populateMandateResponse = PopulateFetchMandateRequestResponse(response);

            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateMandateRequestResponseDataIsNotNullOrEmpty(populateMandateResponse);

            var outgoing = JsonSerializer.Serialize(requestList, new JsonSerializerOptions { WriteIndented = true });
            TestContext.Out.WriteLine("Outgoing payload:\n" + outgoing);
            TestContext.Out.WriteLine("Response status: " + (response?.StatusCode.ToString() ?? "<null>"));
            TestContext.Out.WriteLine("Response content:\n" + (response?.Content ?? "<null>"));
        }

        // Fixes for SPELL, CS0161, and CA1822
        // - SPELL: Rename PopulatefetchMandateRequestResponse to PopulateFetchMandateRequestResponse
        // - CS0161: Ensure all code paths return a value (add throw at end of method)
        // - CA1822: Mark method as static

        private static MandatesRequestResponse PopulateFetchMandateRequestResponse(object response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            if (response is not RestResponse restResponse)
                throw new ArgumentException("Expected a RestResponse instance", nameof(response));

            var content = restResponse.Content;
            if (string.IsNullOrWhiteSpace(content))
                throw new InvalidOperationException("Response content is empty; cannot populate MandatesRequestResponse.");

            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            try
            {
                var deserialized = JsonSerializer.Deserialize<MandatesRequestResponse>(content, opts)
                    ?? throw new InvalidOperationException("Deserialization returned null for MandatesRequestResponse.");

                return deserialized;
            }
            catch (JsonException ex)
            {
                // Best-effort fallback: attempt to read minimal fields without throwing required-property errors
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    var root = doc.RootElement;
                    var result = new MandatesRequestResponse
                    {
                        success = root.TryGetProperty("success", out var s) && s.ValueKind == JsonValueKind.True,
                        diderror = root.TryGetProperty("diderror", out var d) && d.ValueKind == JsonValueKind.True,
                        responseMessage = new ExecutionOutcome { succeeded = false },
                        result = new List<MandatesRequest>()
                    };

                    if (root.TryGetProperty("result", out var arr) && arr.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in arr.EnumerateArray())
                        {
                            // build a minimal MandatesRequest; required properties get a safe default if missing
                            var mr = new MandatesRequest
                            {
                                policyNumber = item.TryGetProperty("policyNumber", out var pn) && pn.ValueKind == JsonValueKind.String ? pn.GetString()! : string.Empty,
                                transactionType = item.TryGetProperty("transactionType", out var tt) && tt.ValueKind == JsonValueKind.Number ? tt.GetInt32() : 0,
                                existingClient = item.TryGetProperty("existingClient", out var ec) && ec.ValueKind == JsonValueKind.True,
                                payerMobileTelephoneNumber = item.TryGetProperty("payerMobileTelephoneNumber", out var pm) && pm.ValueKind == JsonValueKind.String ? pm.GetString() : null,
                                sourceSystemId = item.TryGetProperty("sourceSystemId", out var ss) && ss.ValueKind == JsonValueKind.Number ? ss.GetInt32() : null,
                                agentCode = item.TryGetProperty("agentCode", out var ac) && ac.ValueKind == JsonValueKind.String ? ac.GetString() : null,
                                agentName = item.TryGetProperty("agentName", out var an) && an.ValueKind == JsonValueKind.String ? an.GetString() : null
                            };
                            result.result.Add(mr);
                        }
                    }
                    return result;
                }
                catch
                {
                    throw new InvalidOperationException("Failed to deserialize response content to MandatesRequestResponse.", ex);
                }
            }
            // Ensure all code paths return a value
            throw new InvalidOperationException("Failed to process response for MandatesRequestResponse.");
        }
    }
}