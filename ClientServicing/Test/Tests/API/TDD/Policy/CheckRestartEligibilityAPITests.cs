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
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class CheckRestartEligibilityAPITests : CheckRestartEligibilityValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_CheckRestartEligibilityRequestPayloadIsValid_When_CheckRestartEligibilityAsync_Then() {
            //Arrange            
            var checkRestartEligibilityRequest = JsonSerializer.Deserialize<CheckRestartEligibilityRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "CheckRestartEligibilityRequestPayloadIsValid.json"));
            ValidateCheckRestartEligibilityRequest(checkRestartEligibilityRequest);

            //Act
            var response = await policyAPIClient.CheckRestartEligibilityAsync (checkRestartEligibilityRequest);
            var checkRestartEligibilityResponse = populateCheckRestartEligibilityResponse(response);
            var schema = ResponseSchemasEnvelope.EligibilitySchema;

            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponseDataShouldAcceptValidNames_And_Types(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateCheckRestartEligibilityResponse(checkRestartEligibilityResponse);
        }
    }
}
