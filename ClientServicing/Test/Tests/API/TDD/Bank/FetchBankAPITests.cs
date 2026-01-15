using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    [TestFixture]
    public class FetchBankAPITests : FetchBankResponseValidationMethods
    {
        BankAPIClient bankAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();
        public static IEnumerable<FetchBanksRequest> fetchBankRequestEachTestDataObject = new JsonDataLoader().LoadJsonDataObjects<FetchBanksRequest>("Bank/Data", "AvailableBanks.json");
        public static IEnumerable<TestCaseData> fetchBankRequestEachTestDataFields = new JsonDataLoader().LoadJsonDataFields("Bank/Data", "FetchBanksRequestIsNotNull.json");

        [Test, Category("Positive")]
        public async Task Given_BankRequestIsNotNull_When_FetchBanksAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            FetchBanksRequest? fetchBankRequest = JsonSerializer.Deserialize<FetchBanksRequest>
                (utilitiesHelper.ReadTestDataJson("Bank/Data", "FetchBanksRequestIsNotNull.json"));
            fetchBankRequest.lastChanged = DateTime.Now.AddDays(-10);

            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assertl
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatFetchBanksResponseDataIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
        }
        [Test, Category("Positive")]
        public async Task Given_BankRequestIsNull_When_FetchBanksAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            FetchBanksRequest? fetchBankRequest = JsonSerializer.Deserialize<FetchBanksRequest>
                (utilitiesHelper.ReadTestDataJson("Bank/Data", "FetchBanksRequestIsNull.json"));

            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatFetchBanksResponseDataIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
        }
       [TestCaseSource(nameof(fetchBankRequestEachTestDataObject)), Category("Positive")]
        public async Task Given_FetchBankRequestEachAvailableBank_When_FetchBanksAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid(FetchBanksRequest fetchBankRequest)
        {
            //Arrange
            //ValidateObjectRequestDataIsNotNullOrEmptyOrLessThanZero(fetchBankRequest);
            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assertl
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatFetchBanksResponseDataIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
        }

        [Test, Category("Positive")]
        [TestCaseSource(nameof(fetchBankRequestEachTestDataFields))]
        public async Task Given_FetchBankRequestEachTestDataField_When_FetchBanksAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid(
            string fieldName, object? fieldValue)
        {
            TestContext.Out.WriteLine($"{fieldName} : {fieldValue}");
            //Arrange
            var fetchBankRequest = new FetchBanksRequest();
            switch (fieldName)
            {
                case "bankID":          fetchBankRequest.bankID = fieldValue != null ? Convert.ToInt32(fieldValue.ToString()) : null; break;
                case "bankName":        fetchBankRequest.bankName = fieldValue != null ? fieldValue.ToString() : null; break;
                case "bankShortName":   fetchBankRequest.bankShortName = fieldValue != null ? fieldValue.ToString() : null; break;
                case "dispSeq":         fetchBankRequest.dispSeq = fieldValue != null ? Convert.ToInt32(fieldValue.ToString()) : null; break;
                case "isActive":        fetchBankRequest.isActive = fieldValue != null ? Convert.ToBoolean(fieldValue.ToString()) : null; break;
                case "lastChanged":     fetchBankRequest.lastChanged = fieldValue != null ? Convert.ToDateTime(fieldValue.ToString()) : null; break;
                case "userID":          fetchBankRequest.userID = fieldValue != null ? fieldValue.ToString() : null; break;
                default: TestContext.Out.WriteLine($"Unkown property: {fieldName}"); break;
            }
            //Act
            var response = await bankAPIClient.FetchBanksAsync(fetchBankRequest);
            var fetchBanksResponse = populateFetchBanksResponse(response);

            //Assertl
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatFetchBanksResponseDataIsNotNullOrEmpty(fetchBanksResponse);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "FetchBankResponseSchema.json");
            
        }
    }
}
