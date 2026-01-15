using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class GetPolicyAndMainMemberDetailsByPolicyNumberAPITests : GetPolicyAndMainMemberDetailsByPolicyNumberValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_GetPolicyAndMainMemberDetailsByPolicyNumberRequestPayload_When_GetPolicyAndMainMemberDetailsByPolicyNumberAsync_Then_()
        {
            //Arrange
            var getPolicyAndMainMemberDetailsByPolicyNumber = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetPolicyAndMainMemberDetailsByPolicyNumberRequestPayload.json"));

            //Act
            var response = await policyAPIClient.GetPolicyAndMainMemberDetailsByPolicyNumberAsync(getPolicyAndMainMemberDetailsByPolicyNumber);
            var getPolicyAndMainMemberDetailsByPolicyNumberResponse = populateGetPolicyAndMainMemberDetailsByPolicyNumberResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetPolicyAndMainMemberDetailsByPolicyNumberResponseDataIsNotNullOrEmpty(getPolicyAndMainMemberDetailsByPolicyNumberResponse);
        }
        private GetPolicyAndMainMemberDetailsByPolicyNumberResponse populateGetPolicyAndMainMemberDetailsByPolicyNumberResponse(RestResponse response) {
            var getPolicyAndMainMemberDetailsByPolicyNumberResponse = new GetPolicyAndMainMemberDetailsByPolicyNumberResponse {
                executionOutcome = new ExecutionOutcome(),
                data = new PolicyAndMainMemberDetails()
            };
            using JsonDocument jsDoc = JsonDocument.Parse (response.Content);

            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name) {
                    case "succeeded":   getPolicyAndMainMemberDetailsByPolicyNumberResponse.executionOutcome.succeeded =    (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     getPolicyAndMainMemberDetailsByPolicyNumberResponse.executionOutcome.message =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      getPolicyAndMainMemberDetailsByPolicyNumberResponse.executionOutcome.errors =       utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var policyAndMainMemberDetails = new PolicyAndMainMemberDetails();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "policy_NO":           policyAndMainMemberDetails.policy_NO =          (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "entityNo":            policyAndMainMemberDetails.entityNo =           (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "legacy_Pol_No":       policyAndMainMemberDetails.legacy_Pol_No =      utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "annualIncrease":      policyAndMainMemberDetails.annualIncrease =     (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "dateOfCommencement":  policyAndMainMemberDetails.dateOfCommencement = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "reInstatedDate":      policyAndMainMemberDetails.reInstatedDate =     (DateTime?)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "lapsedDate":          policyAndMainMemberDetails.lapsedDate =         (DateTime?)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "venue":               policyAndMainMemberDetails.venue =              utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "salesPerson":         policyAndMainMemberDetails.salesPerson =        utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "campaignCode":        policyAndMainMemberDetails.campaignCode =       (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "policyFee":           policyAndMainMemberDetails.policyFee =          (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "captureDate":         policyAndMainMemberDetails.captureDate =        (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "preferedCommunicationMethod":  policyAndMainMemberDetails.preferedCommunicationMethod = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "masterContract":      policyAndMainMemberDetails.masterContract =     utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "title":               policyAndMainMemberDetails.title =              utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "titleID":             policyAndMainMemberDetails.titleID =            (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "firstname":           policyAndMainMemberDetails.firstname =          utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "surname":             policyAndMainMemberDetails.surname =            utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "legalRefNo":          policyAndMainMemberDetails.legalRefNo =         utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "legalNumberType":     policyAndMainMemberDetails.legalNumberType =    (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "dateOfBirth":         policyAndMainMemberDetails.dateOfBirth =        (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "preferredTelTypeCd":  policyAndMainMemberDetails.preferredTelTypeCd = (int?)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "faxNumber":           policyAndMainMemberDetails.faxNumber =          (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "homeNumber":          policyAndMainMemberDetails.homeNumber =         utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "emailAddress":        policyAndMainMemberDetails.emailAddress =       (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "cellNumber":          policyAndMainMemberDetails.cellNumber =         utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "workNumber":          policyAndMainMemberDetails.workNumber =         (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "alternateNumber":     policyAndMainMemberDetails.alternateNumber =    (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "whatsappNumber":      policyAndMainMemberDetails.whatsappNumber =     (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddress1":    policyAndMainMemberDetails.physicalAddress1 =   utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalAddress2":    policyAndMainMemberDetails.physicalAddress2 =   (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalSuburb":      policyAndMainMemberDetails.physicalSuburb =     utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalTown":        policyAndMainMemberDetails.physicalTown =       utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "physicalPostalCode":  policyAndMainMemberDetails.physicalPostalCode = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddress1":      policyAndMainMemberDetails.postalAddress1 =     utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalAddress2":      policyAndMainMemberDetails.postalAddress2 =     (string?)utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalSuburb":        policyAndMainMemberDetails.postalSuburb =       utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "postalTown":          policyAndMainMemberDetails.postalTown =         utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "genderCD":            policyAndMainMemberDetails.genderCD =           (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "smokerCd":            policyAndMainMemberDetails.smokerCd =           (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "smokerDescr":         policyAndMainMemberDetails.smokerDescr =        utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "lastBillingDate":     policyAndMainMemberDetails.lastBillingDate =    (DateTime?)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "lastPaidDate":        policyAndMainMemberDetails.lastPaidDate =       (DateTime?)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "nextBillingDate":     policyAndMainMemberDetails.nextBillingDate =    (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "policyPremiumAmount": policyAndMainMemberDetails.policyPremiumAmount = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "premiumCount":        policyAndMainMemberDetails.premiumCount =       (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "paymentFrequency":    policyAndMainMemberDetails.paymentFrequency =   utilitiesHelper.ReadStringNullable(item.Value); break;
                            }
                        }
                        getPolicyAndMainMemberDetailsByPolicyNumberResponse.data = policyAndMainMemberDetails;
                        break;
                    }
                }
                return getPolicyAndMainMemberDetailsByPolicyNumberResponse;
        }
    }
}