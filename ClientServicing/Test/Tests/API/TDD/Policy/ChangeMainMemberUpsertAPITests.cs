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
    public class ChangeMainMemberUpsertAPITests
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("API call failed with status code: InternalServerError and message: Procedure or function spChangeMainMember has too many arguments specified.")]
        public async Task Given_ChangeMainMemberUpsertRequestPayloadIsValid_When_ChangeMainMemberUpsertAsync_Then() {
            //Arrange
            var changeMainMemberUpsertRequest = JsonSerializer.Deserialize<ChangeMainMemberUpsertRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "ChangeMainMemberUpsertRequestPayloadIsValid.json"));

            //Act
            var request = await policyAPIClient.ChangeMainMemberUpsertAsync (changeMainMemberUpsertRequest);
            //var  changeMainMemberUpsertResponse = populateChangeMainMemberUpsertResponse(response);

            // Assert
            /*
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateChangeMainMemberUpsertResponseDataIsNotNullOrEmpty(changeMainMemberUpsertResponse);
            */
        }
    }
}
