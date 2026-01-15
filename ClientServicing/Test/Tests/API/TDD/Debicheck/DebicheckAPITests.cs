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

            //Arrange
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CheckStatusRequest checkstatusrequest = JsonSerializer.Deserialize<CheckStatusRequest>
                (utilitiesHelper.ReadTestDataJson("Debicheck\\Data", "CheckStatusRequest.json"), opts)
                ?? throw new InvalidOperationException("Deserialization of Check Status Request returned null");

            // Validate test payload before sending (fail fast with clear message)
            if (string.IsNullOrWhiteSpace(checkstatusrequest.policyNumber))
                throw new InvalidOperationException("Test payload missing required property: policyNumber");

            if (checkstatusrequest.sourceSystemId <= 0)
                throw new InvalidOperationException("Test payload has invalid sourceSystemId; must be > 0");

            var outgoing = JsonSerializer.Serialize(checkstatusrequest, new JsonSerializerOptions { WriteIndented = true });
            TestContext.Out.WriteLine("Outgoing payload object:\n" + outgoing);

            //Act
            var response = await debicheckAPIclient.DebicheckAPIClientAsync<CheckStatusRequest>(checkstatusrequest);

            // Log response for diagnostics
            TestContext.Out.WriteLine($"Response status: {(response?.StatusCode.ToString() ?? "<null>")}");
            TestContext.Out.WriteLine("Response content:\n" + (response?.Content ?? "<null>"));

            // Assert status code and include response body in the failure message so it's easy to diagnose 400s
            Assert.That(response?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK),
                "Expected HTTP 200 OK. Response content: " + (response?.Content ?? "<no content>"));

            var fetchCheckStatusResponse = populatefetchCheckStatusResponse(response);

            ValidationAssertionHeading();
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(fetchCheckStatusResponse);
        }
    }
}