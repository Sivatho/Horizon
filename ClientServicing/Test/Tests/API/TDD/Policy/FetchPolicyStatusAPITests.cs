using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class FetchPolicyStatusAPITests : FetchPolicyStatusValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_FetchPolicyStatusRequestPayloadIsValid_When_FetchPolicyStatusAsync_Then_ValidateFetchPolicyStatusResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            var policyNoRequest = JsonSerializer.Deserialize<PolicyNoRequest>(
                utilitiesHelper.ReadTestDataJson("General/Data", "ListOfPolicyNo.json"));
            int firstPolicyNo = policyNoRequest.policyNoList[0];
            var request = new PolicyNoRequest
            {
                policyNo = firstPolicyNo
            };
            ValidateFetchPolicyStatusRequestDataIsNotNullOrEmpty(request);

            // Act
            var response = await policyAPIClient.FetchPolicyStatusAsync(request);
            var fetchPolicyStatusResponse = populateFetchPolicyStatusResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateFetchPolicyStatusResponseDataIsNotNullOrEmpty(fetchPolicyStatusResponse);
            //ValidateResponseSchemaIsValid(response, "Policy/Schema", "FetchPolicyStatusResponseSchema.json");
        }
    }
}
