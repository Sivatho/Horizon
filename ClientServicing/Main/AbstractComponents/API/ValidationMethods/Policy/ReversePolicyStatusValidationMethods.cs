using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class ReversePolicyStatusValidationMethods : AbstractValidationMethods, IReversePolicyStatusValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public CheckHasProductResponse populateReversePolicyStatusResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var reversePolicyStatusResponse = new CheckHasProductResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": reversePolicyStatusResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": reversePolicyStatusResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": reversePolicyStatusResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data": reversePolicyStatusResponse.data = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return reversePolicyStatusResponse;
        }
        public void ValidationReversePolicyStatusRequestIsNotNullOrEmptyAndIsNotLessThanZero(ReversePolicyStatusRequest reversePolicyStatusRequest)
        {
            using (Assert.EnterMultipleScope()) { 
                Assert.That(reversePolicyStatusRequest.policyNo,        Is.Not.LessThan(0),     "PolicyNo Should Not Be Less Than Zero");
                Assert.That(reversePolicyStatusRequest.effectiveDate,   Is.Not.Null.Or.Empty,   "EffectiveDate Should Not Be NUll Or Empty");
                Assert.That(reversePolicyStatusRequest.noteText,        Is.Not.Null.Or.Empty,   "NoteText Should Not Be NUll Or Empty");
            }
            DocumentTemplate.DisplayBody("Validated: ReversePolicyStatusRequest Data Is Not Null Or Empty And Integer Is Not Less Than Zero");
        }
        public void ValidationReversePolicyStatusResponseIsNotNullOrEmpty(CheckHasProductResponse reversePolicyStatusResponse)
        {
            Assert.Multiple(() => {
                Assert.That(reversePolicyStatusResponse.executionOutcome,   Is.Not.Null.Or.Empty, "ExecutionOutcome Should Not Be Null Or Empty");
                Assert.That(reversePolicyStatusResponse.data,               Is.Not.Null.Or.Empty, "Data Should Not Be Null Or Empty");
            });
            DocumentTemplate.DisplayBody("Validated: ReversePolicyStatusResponse Should Not Be Null Or Empty");
        }
    }
}
