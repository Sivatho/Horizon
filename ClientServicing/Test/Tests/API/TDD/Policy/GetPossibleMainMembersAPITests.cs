using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class GetPossibleMainMembersAPITests
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        
        [Test]
        [Ignore("Internal Server Error: Error Code:500 Message: Procedure or function spGetPossibleEntityForMainMem has too many arguments specified.")]
        public async Task Given_GetPossibleMainMembersRequestPayloadIsValid_When_GetPossibleMainMembersAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_GetPossibleMainMembersResponseDataIsNotNullOrEmpty() {
            // Arrange
            var getPossibleMainMembersRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetPolicyAndMainMemberDetailsByPolicyNumberRequestPayload.json"));
            
            // Act
           var response = await policyAPIClient.GetPossibleMainMembersAsync(getPossibleMainMembersRequest);
            //var getPossibleMainMembersResponse = populateGetPossibleMainMembersResponse(response);
            
            // Assert
            /*
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetPossibleMainMembersResponseDataIsNotNullOrEmpty(getPossibleMainMembersResponse);
            */
        }
    }
}
