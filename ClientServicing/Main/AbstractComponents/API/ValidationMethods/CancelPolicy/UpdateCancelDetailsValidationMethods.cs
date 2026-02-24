using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.CancelPolicy;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.CancelPolicy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.CancelPolicy
{
    public class UpdateCancelDetailsValidationMethods : AbstractValidationMethods, IUpdateCancelDetailsValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public void ValidateUpdateCancelDetailsRequestIsNotNullOrEmpty(UpdateCancelPolicyDetailsRequest updateCancelPolicyDetailsRequest)
        {
            Assert.Multiple(() => {
                Assert.That(updateCancelPolicyDetailsRequest,                   Is.Not.Null.Or.Empty,                       "Response: updateCancelPolicyDetailsRequest Should Not Be Null Or Empty");
                Assert.That(updateCancelPolicyDetailsRequest.policyNo,          Is.Not.LessThan(0),                         "Response: policyNo Should Not Be Less Than 0");
                Assert.That(updateCancelPolicyDetailsRequest.statusCd,          Is.Not.LessThan(0),                         "Response: statusCd Should Not Be Less Than 0");
                Assert.That(updateCancelPolicyDetailsRequest.reasonCd,          Is.Not.LessThan(0),                         "Response: statusCd Should Not Be Less Than 0");
                Assert.That(updateCancelPolicyDetailsRequest.subReasonCd,       Is.Not.LessThan(0),                         "Response: subReasonCd Should Not Be Less Than 0");
                Assert.That(updateCancelPolicyDetailsRequest.effectiveDate,     Is.Null.Or.Not.EqualTo(default(DateTime)),  "Response: effectiveDate Should Not Be Null Or Not Equal To Default DateTime");
                Assert.That(updateCancelPolicyDetailsRequest.comment,           Is.Null.Or.TypeOf<string>(),                "Response: comment Should Be Null Or Type Of string");
                Assert.That(updateCancelPolicyDetailsRequest.userID,            Is.Null.Or.TypeOf<string>(),                "Response: userID Should Be Null Or Type Of string");
                Assert.That(updateCancelPolicyDetailsRequest.paymentTypeCD,     Is.Not.LessThan(0),                         "Response: paymentTypeCD Should Not Be Less Than 0");
                Assert.That(updateCancelPolicyDetailsRequest.providerReference, Is.Null.Or.TypeOf<string>(),                "Response: providerReference Should Be Null Or Type Of string");
            });
            DocumentTemplate.DisplayBody("Validated: UpdateCancelPolicyDetailsRequest: Is Not Null Or Empty And Integer Is Not Less Than 0 And DateTime Is Not Equal To Default");

        }

        public void ValidateUpdateCancelDetailsResponseIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse updateCancelPolicyDetailsResponse)
        {
            Assert.Multiple(() => {
                Assert.That(updateCancelPolicyDetailsResponse, Is.Not.Null.Or.Empty, "Response: updateCancelPolicyDetailsResponse Should Not Be Null Or Empty");
                Assert.That(updateCancelPolicyDetailsResponse.executionOutcome, Is.Not.Null.Or.Empty, "Response: updateCancelPolicyDetailsResponse.executionOutcome Should Not Be Null Or Empty");

                Assert.That(updateCancelPolicyDetailsResponse.executionOutcome.succeeded,   Is.True.Or.False,              "Response: Succeeded Should Be True or False.");
                Assert.That(updateCancelPolicyDetailsResponse.executionOutcome.message,     Is.Null.Or.TypeOf<string?>(),   "Response: Message Should Be Null Or Type Of String.");
                Assert.That(updateCancelPolicyDetailsResponse.executionOutcome.errors,      Is.Null.Or.TypeOf<string?>(),   "Response: Errors Should Be Null Or Type Of String.");
                Assert.That(updateCancelPolicyDetailsResponse.executionOutcome.succeeded,   Is.True.Or.False, "Response: Succeeded Should Be True or False.");
            });
            DocumentTemplate.DisplayBody("Validated: PolicyEntityInfoUpsertResponse: Is Not Null Or Type Of String And bool Is True Or False");

        }

        public PolicyEntityInfoUpsertResponse PopulateUpdateCancelDetailsResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var entityInfoUpsertResponse = new PolicyEntityInfoUpsertResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   entityInfoUpsertResponse.executionOutcome.succeeded =   (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     entityInfoUpsertResponse.executionOutcome.message =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      entityInfoUpsertResponse.executionOutcome.errors =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        entityInfoUpsertResponse.data =                         (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return entityInfoUpsertResponse;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }       
    }
}
