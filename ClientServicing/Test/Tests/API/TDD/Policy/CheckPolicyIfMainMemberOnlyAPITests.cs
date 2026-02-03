using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class CheckPolicyIfMainMemberOnlyAPITests : CheckPolicyIfMainMemberOnlyValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [TestCase, Category("Postive")]
        public async Task Given_PolicyNoOfMainMemberOnlyIsValid_When_CheckPolicyIfMainMemberOnlyAsync_ThenValidateGetUnmentPremiumResponseIsOk_And_DataIsNotNullOrEmpty() {
            //Arrange
            var checkPolicyIfMainMemberOnlyRequest = JsonSerializer.Deserialize<PolicyNoRequest>(
                utilitiesHelper.ReadTestDataJson("General/Data", "ListOfPoliciesOfMainMemberOnly.json"));
            int firstPolicyNo = checkPolicyIfMainMemberOnlyRequest.policyNoList[0];
            checkPolicyIfMainMemberOnlyRequest.policyNo = firstPolicyNo;
            ValidationCheckPolicyIfMainMemberOnlyRequest(checkPolicyIfMainMemberOnlyRequest);

            //Act
            var response = await policyAPIClient.CheckPolicyIfMainMemberOnlyAsync(firstPolicyNo);
            var checkPolicyIfMainMemberOnlyResponse = populateCheckPolicyIfMainMemberOnlyResponse(response);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponseDataShouldAcceptValidNames_And_Types(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidationCheckPolicyIfMainMemberOnlyResponse(checkPolicyIfMainMemberOnlyResponse);
        }
    }
}
