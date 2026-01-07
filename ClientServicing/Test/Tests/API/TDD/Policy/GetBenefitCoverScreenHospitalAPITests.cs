using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    public class GetBenefitCoverScreenHospitalAPITests
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("API call failed with status code: InternalServerError and message: \"Invalid object name 'Claims.claim.ClaimBenefit\"")]
        public async Task Given_GetBenefitCoverScreenHospitalRequestPayloadIsValid_When_GetBenefitCoverScreenHospitalAsync_Then()
        {
            //Arrange
            var getBenefitCoverScreenHospitalRequest = JsonSerializer.Deserialize<GetBenefitCoverScreenRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetBenefitCoverScreenRequestPayloadIsValid.json"));

            //Act
            var response = await policyAPIClient.GetBenefitCoverScreenHospitalAsync(getBenefitCoverScreenHospitalRequest);
            //var getBenefitCoverScreenHospitalResponse = populateGetBenefitCoverScreenHospitalResponse(response);

            // Assert
            /*
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetBenefitCoverScreenHospitalResponseDataIsNotNullOrEmpty(getBenefitCoverScreenHospitalResponse);
            */
        }
    }
}
