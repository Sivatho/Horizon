using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    [TestFixture]
    public class PolicyBeneficiaryDetailsAPITest : PolicyBeneficiaryDetailsValidationMethods
    {

        BeneficiaryDetailsAPIClient? beneficiaryDetailsAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            beneficiaryDetailsAPIClient = new BeneficiaryDetailsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test]
        public async Task Given_PolicyBeneficiaryDetailsRequest_When_PolicyBeneficiaryDetailsAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange            
            var beneficiaryDetailsRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>(utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyBeneficiaryDetailsPayloadIsValid.json"));
            var schema = ResponseSchemasEnvelope.PolicyBeneficiaryDetailsSchema;
            ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(beneficiaryDetailsRequest!);
            // Act
            var response = await beneficiaryDetailsAPIClient!.PolicyBeneficiaryDetailsAsync(beneficiaryDetailsRequest!);
            var policyBeneficiaryDetailsResponse = PopulatePolicyBeneficiaryDetailsResponse(response);
            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(policyBeneficiaryDetailsResponse);

        }
    }
}
