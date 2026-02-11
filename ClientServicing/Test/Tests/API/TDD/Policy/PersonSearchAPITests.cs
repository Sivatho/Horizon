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
    public class PersonSearchAPITests : PersonSearchValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_PersonSearchRequestPayloadIsValid_When_PersonSearchAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_PersonSearchResponseDataIsNotNullOrEmpty() {
            // Arrange
            var personSearch = JsonSerializer.Deserialize<PersonSearchRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "PersonSearchRequestPayloadIsValid.json"));
            
            // Act
            var response = await policyAPIClient.PersonSearchAsync (personSearch);
            var personSearchDetails = populatePersonSearch(response);

            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePersonSearchResponseDataIsNotNullOrEmpty(personSearchDetails);
            ValidateResponseSchemaIsValid(response, "Policy/Schema", "PersonSearchResponseSchema.json");
        }
        private PersonSearchResponse populatePersonSearch(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var personSearchResponse = new PersonSearchResponse
            {
                executionOutcome = new ExecutionOutcome(),
                personSearchDetails = new List<PersonSearchDetails>()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": personSearchResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": personSearchResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": personSearchResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            var personSearchDetails = new PersonSearchDetails
                            {
                                entityID =          (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("entityID")),
                                ifaNo =             (int?)utilitiesHelper.ReadInt32Nullable(item.GetProperty("ifaNo")),
                                entityNo =          (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("entityNo")),
                                entityName =        utilitiesHelper.ReadStringNullable(item.GetProperty("entityName")),
                                entitySurname =     utilitiesHelper.ReadStringNullable(item.GetProperty("entitySurname")),
                                entityDOB =         (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("entityDOB")),
                                legalRefNo =        utilitiesHelper.ReadStringNullable(item.GetProperty("legalRefNo")),
                                legalRefNoType =    utilitiesHelper.ReadStringNullable(item.GetProperty("legalRefNoType")),
                                citizenshipCD =     (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("citizenshipCD")),
                                alpha3Code =        utilitiesHelper.ReadStringNullable(item.GetProperty("alpha3Code")),
                                citizenship =       utilitiesHelper.ReadStringNullable(item.GetProperty("citizenship")),
                                emailAddress =      utilitiesHelper.ReadStringNullable(item.GetProperty("emailAddress")),
                                cellphoneNumber =   utilitiesHelper.ReadStringNullable(item.GetProperty("cellphoneNumber")),
                                physicalAddress1 =  utilitiesHelper.ReadStringNullable(item.GetProperty("physicalAddress1")),
                                legacyPolicyNo =    utilitiesHelper.ReadStringNullable(item.GetProperty("legacyPolicyNo")),
                                policyNo =          (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("policyNo")),
                                roleCd =            (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("roleCd")),
                                status =            utilitiesHelper.ReadStringNullable(item.GetProperty("status")),
                                statusCD =          (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("statusCD")),
                                planTypeDescr =     utilitiesHelper.ReadStringNullable(item.GetProperty("planTypeDescr")),
                                statusDate =        (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("statusDate")),
                                dateOfCommencement = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("dateOfCommencement")),
                                premiumAmt =        (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("premiumAmt")),
                                salesPerson =       utilitiesHelper.ReadStringNullable(item.GetProperty("salesPerson")),
                                rewardStatus =      utilitiesHelper.ReadStringNullable(item.GetProperty("rewardStatus")),
                                debiCheckStatus =   utilitiesHelper.ReadStringNullable(item.GetProperty("debiCheckStatus")),
                                agency =            utilitiesHelper.ReadStringNullable(item.GetProperty("agency")),
                                payor =             utilitiesHelper.ReadStringNullable(item.GetProperty("payor")),
                                payorLegalReferenceNumber = utilitiesHelper.ReadStringNullable(item.GetProperty("payorLegalReferenceNumber")),
                                payorCellphoneNumber = utilitiesHelper.ReadStringNullable(item.GetProperty("payorCellphoneNumber")),
                                payorEmailAddress = (string?)utilitiesHelper.ReadStringNullable(item.GetProperty("payorEmailAddress")),
                                beneficiaryName =   utilitiesHelper.ReadStringNullable(item.GetProperty("beneficiaryName")),
                                paymentTypeCD =     (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("paymentTypeCD")),
                                inspiratorNo =      utilitiesHelper.ReadStringNullable(item.GetProperty("inspiratorNo")),
                                region =            utilitiesHelper.ReadStringNullable(item.GetProperty("region")),
                                partnerCD =         (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("partnerCD")),
                                partnerCode =       utilitiesHelper.ReadStringNullable(item.GetProperty("partnerCode")),
                                schemeCD =          (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("schemeCD")),
                                schemeDesc =        utilitiesHelper.ReadStringNullable(item.GetProperty("schemeDesc")),
                                planCD =            (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("planCD")),
                                planDesc =          utilitiesHelper.ReadStringNullable(item.GetProperty("planDesc")),
                                channelCD =         (int?)utilitiesHelper.ReadInt32Nullable(item.GetProperty("channelCD")),
                                channelDesc =       (string?)utilitiesHelper.ReadStringNullable(item.GetProperty("channelDesc")),
                                entityFullname =    utilitiesHelper.ReadStringNullable(item.GetProperty("entityFullname"))
                            };
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return personSearchResponse;
        }
    }
}
