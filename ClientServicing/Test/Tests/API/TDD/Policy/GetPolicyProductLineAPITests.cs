using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class GetPolicyProductLineAPITests : GetPolicyProductLineValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test, Category("Positive")]
        public async Task Given_GetPolicyProductLineRequestIsValid_When_GetPolicyProductLineAsync_Then_Then_ValidateGetPolicyProductLineResponseIsOk_And_DataIsNotNullOrEmpty_And_IsNotLessThanZero() {
            //Arrange
            var getPolicyProductLineRequest = JsonSerializer.Deserialize<PolicyNoAndLegacyPolicyNumberRequest>(
                utilitiesHelper.ReadTestDataJson("Policy/Data", "GetPolicyAndMainMemberDetailsByPolicyNumberRequestPayload.json"));
            ValidateGetPolicyProductLineRequestDataIsNotNullOrEmpty_NotLessThanZero(getPolicyProductLineRequest);

            //Act
            var response = await policyAPIClient.GetPolicyProductLineAsync(getPolicyProductLineRequest);
            var getPolicyProductLineResponse = populateGetPolicyProductLineResponse(response);
            var schema = ResponseSchemasEnvelope.GetPolicyProductLineSChema;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateGetPolicyProductLineResponseDataIsNotNullOrEmpty_NotLessThanZero(getPolicyProductLineResponse);
        }
    }
}
