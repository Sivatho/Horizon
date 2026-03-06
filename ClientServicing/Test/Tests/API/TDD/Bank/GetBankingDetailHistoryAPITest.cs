using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    [TestFixture]
    public class GetBankingDetailHistoryAPITest : GetBankDetailsHisoryResponseValidationMethods
    {
        BankAPIClient bankAPIClient = null!;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            bankAPIClient = new BankAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        public static IEnumerable<TestCaseData> fetchBankRequestEachTestDataFields = new JsonDataLoader().ReadJsonTestDataFields("Bank/Data", "BankDetailsAreNotNull.json");
        [Test, Category("Positive")]
        public async Task Given_PolicyNumberValid_When_GetBankingDetailHistoryAsync_Then_ValidateGetBankDetailHistoryResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            var policyNoRequest = JsonSerializer.Deserialize<PolicyNoRequest>(
                utilitiesHelper.ReadTestDataJson("General/Data", "ListOfPolicyNo.json"));

            int policyNumber = policyNoRequest!.policyNoList[0];
            policyNoRequest.policyNo = policyNumber;
            ValidateGetBankDetailsHisoryResponseRequestIsNotNullOrEmy(policyNoRequest!);
            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = PopulateGetBankDetailHistoryResponse(response);
            var schema = ResponseSchemasEnvelope.GetBankingDetailHistorySchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountGreaterThanZero(getBankDetailHistoryResponse);

        }
        [Test, Category("Positive")]
        public async Task Given_PolicyNumberIsInvalid_When_GetBankingDetailHistoryAsync_Then_ValidateGetBankDetailHistoryResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            int policyNumber = -1;

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
            var getBankDetailHistoryResponse = PopulateGetBankDetailHistoryResponse(response);
            var schema = ResponseSchemasEnvelope.GetBankingDetailHistorySchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountLessThanOrEqualToZero(getBankDetailHistoryResponse);
        }
    }
}
