using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class PrePopulateEntityInfoByIDAPITests : PrePopulateEntityInfoByIDvalidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_CheckHasProductRequestPayloadIsValid_When_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_PrePopulateEntityInfoByIDResponseDataIsNotNullOrEmpty()
        {
            //Arrange
            var prePopulateEntityInfoByIDRequest = JsonSerializer.Deserialize<CheckHasProductRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "CheckHasProductRequestPayloadIsValid.json"));

            //Act
            var response = await policyAPIClient.PrePopulateEntityInfoByIDAsync(prePopulateEntityInfoByIDRequest);
            var prePopulateEntityInfoByIDResponse = PopulatePrePopulateEntityInfoByIDResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePrePopulateEntityInfoByIDResponseDataIsNotNullOrEmpty(prePopulateEntityInfoByIDResponse);
        }
        private PrePopulateEntityInfoByIDResponse PopulatePrePopulateEntityInfoByIDResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            PrePopulateEntityInfoByIDResponse prePopulateEntityInfoByIDResponse = new PrePopulateEntityInfoByIDResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new EntityInfo()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": prePopulateEntityInfoByIDResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": prePopulateEntityInfoByIDResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": prePopulateEntityInfoByIDResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var data = new EntityInfo();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "entityNo": data.entityNo = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "titleCd": data.titleCd = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "titleDescr": data.titleDescr = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "firstName": data.firstName = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "surname": data.surname = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "legalRefNo": data.legalRefNo = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "genderCd": data.genderCd = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "genderDescr": data.genderDescr = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "citizenshipCd": data.citizenshipCd = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "citizenshipDescr": data.citizenshipDescr = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "smokerCd": data.smokerCd = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "smokerDescr": data.smokerDescr = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "cellNumber": data.cellNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "homeNumber": data.homeNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "faxNumber": data.faxNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "workNumber": data.workNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "alternateNumber": data.alternateNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "whatsappNumber": data.whatsappNumber = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "emailAddress": data.emailAddress = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddressLine1": data.postalAddressLine1 = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddressLine2": data.postalAddressLine2 = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddressSuburb": data.postalAddressSuburb = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddressCity": data.postalAddressCity = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddressCode": data.postalAddressCode = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddressLine1": data.physicalAddressLine1 = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddressLine2": data.physicalAddressLine2 = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddressSuburb": data.physicalAddressSuburb = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddressCity": data.physicalAddressCity = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddressCode": data.physicalAddressCode = utilitiesHelper.ReadStringNullable(item.Value); break;
                            }
                        }
                        prePopulateEntityInfoByIDResponse.data = data;
                        break;
                }
            }
            return prePopulateEntityInfoByIDResponse;
        }
    }
}
