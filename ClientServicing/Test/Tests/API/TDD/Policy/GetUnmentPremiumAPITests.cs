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
    public class GetUnmentPremiumAPITests : GetUnmentPremiumValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();
        [Test]
        //[Ignore("API is still in development. Could not find stored procedure 'polmaspr.spGetUnmentPremium'.")]
        public async Task Given_GetUnmentPremiumRequestPayloadIsValid_When_GetUnmentPremiumAsync_Then_ValidateGetUnmentPremiumResponseIsOk_And_DataIsNotNullOrEmpty_And_IsNotLessThanZero_And_DateTimeIsNotEqualToDefault()
        {
            // Arrange           
            var policyNoRequest = JsonSerializer.Deserialize<PolicyNoRequest>(
                utilitiesHelper.ReadTestDataJson("General/Data", "ListOfPolicyNo.json"));
            int firstPolicyNo = policyNoRequest.policyNoList[0];
            

            // Act
            var response = await policyAPIClient.GetUnmentPremiumAsync(firstPolicyNo);
            var getUnmentPremiumResponse = populateGetUnmentPremiumResponse(response);
            var schema = ResponseSchemasEnvelope.GetUnmetPremiumResponseSchema();

            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponseDataShouldAcceptValidNames_And_Types(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateGetUnmentPremiumResponseDataIsNotNullOrEmpty_And_IsNotLessThanZero_And_DateTimeIsNotEqualToDefault(getUnmentPremiumResponse);
        }
    }
}
