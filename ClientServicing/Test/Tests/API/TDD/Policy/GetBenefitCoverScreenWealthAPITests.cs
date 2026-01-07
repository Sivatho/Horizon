using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class GetBenefitCoverScreenWealthAPITests
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("API call failed with status code: InternalServerError and message: \"Invalid object name 'Claims.claim.ClaimBenefit\"")]
        public async Task Given_GetBenefitCoverScreenWealthRequestPayloadIsValid_When_GetBenefitCoverScreenWealthAsync_Then() {
            //Arrange
            var getBenefitCoverScreenWealthRequest = JsonSerializer.Deserialize<GetBenefitCoverScreenRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetBenefitCoverScreenRequestPayloadIsValid.json"));
            //Act
            var request = await policyAPIClient.GetBenefitCoverScreenWealthAsync (getBenefitCoverScreenWealthRequest);
            //var  getBenefitCoverScreenWealthResponse = populateGetBenefitCoverScreenWealthResponse(response);
            
            // Assert
           
            /*
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetBenefitCoverScreenWealthResponseDataIsNotNullOrEmpty(getBenefitCoverScreenWealthResponse);
            */
        }
    }
}
