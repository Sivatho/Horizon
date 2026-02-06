using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BenefitExtendedMember;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Resources.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Test.Tests.API.TDD.BenefitExtendedMember
{
    [TestFixture]
    public class policyBenefitExtendedMemberAPITest : PolicyBenefitExtendedMemberResponseValidationMethods
    {
        BenefitExtendedMemberAPIClient benefitExtendedMemberAPIClient = new BenefitExtendedMemberAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_policyBenefitExtendedMemberRequestPayloadIsValid_When_policyBenefitExtendedMemberResponseCodeOK_And_PropertyNameisValid_And_DataTypesIsValid_And_policyBenefitExtendedMemberResponseDataIsNotNull()
            {
            //Arrange
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            policyBenefitExtendedMemberRequest PolicyBenefitExtendedMemberRequest = JsonSerializer.Deserialize<policyBenefitExtendedMemberRequest>
                (utilitiesHelper.ReadTestDataJson("BenefitExtendedMember\\Data", "policyBenefitExtendedMemberHasPolicyNumber.json"), opts)
                ?? throw new InvalidOperationException("Failed to deserialize test data: policyBenefitExtendedMemberHasPolicyNumber.json");

            var response = await benefitExtendedMemberAPIClient.policyBenefitExtendedMemberAsync(PolicyBenefitExtendedMemberRequest);
            var policyBenefitExtendedMemberResponse = populatePolicyBenefitExtendedMemberResponse(response);

            ValidationAssertionHeading();
            ValidateResponseStatusCode(response,HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(policyBenefitExtendedMemberResponse);

            var outgoing = JsonSerializer.Serialize(PolicyBenefitExtendedMemberRequest, new JsonSerializerOptions { WriteIndented = true });
            TestContext.Out.WriteLine("Outgoing payload object:\n" + outgoing);
        }
    }
}
