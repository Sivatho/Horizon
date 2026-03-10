using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class PolicyEntityInfoUpsertAPITest : PolicyEntityInfoUpsertResponseValidationMethods
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
        [Ignore("Status Code:: InternalServerError\r\n" +
            "Message:: Error converting data type int to smallint.\r\n" +
            "Error converting data type int to smallint.|0|Customer.custpr.spDALEntityRelationInsert\r\n" +
            "@policyMovementTypeCd\r\n" +
            "6\r\n" +
            "35796")]
        public async Task Given_PolicyEntityInfoUpsertRequest_When_PolicyEntityInfoUpsertAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            PolicyEntityInfoUpsertRequest policyEntityInfoUpsert = JsonSerializer.Deserialize<PolicyEntityInfoUpsertRequest>(utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyEntityInfoUpsertRequestPayloadIsValid.json"));
            policyEntityInfoUpsert.dateOfBirth = DateTime.Now.AddYears(-25);
            policyEntityInfoUpsert.effectiveDate = DateTime.Now.AddDays(30);

            // Act
            var response = await beneficiaryDetailsAPIClient.PolicyEntityInfoUpsertAsync(policyEntityInfoUpsert);
            var policyBeneficiaryDetailsResponse = PopulatePolicyEntityInfoUpsert(response);
            // Assert            
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            //ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "PolicyEntityDetailInfoUpertResponseSchema.json");
            ValidatePolicyEntityInfoUpsertResponseDataIsNotNullOrEmpty(policyBeneficiaryDetailsResponse);            
        }        
    }
}
