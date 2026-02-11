using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{    
    public class CheckRestartEligibilityValidationMethods : AbstractValidationMethods, ICheckRestartEligibilityValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateCheckRestartEligibilityRequest(CheckRestartEligibilityRequest checkRestartEligibilityRequest)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(
                    checkRestartEligibilityRequest.policyNo, 
                    Is.Not.LessThan(0),
                    "PolicyNo Should Not Be Less Than Zore");
                Assert.That(
                    checkRestartEligibilityRequest.billingPeriodToCheck, 
                    Is.Not.EqualTo(default(DateTime)),
                    "BillingPeriodToCheck Should Not Be Equal To Default");
            }
            DocumentTemplate.DisplayBody("Data Integers Should Not Be Less Than Zero And Should Not Equal To Default DateTime");
        }
        public void ValidateCheckRestartEligibilityResponse(CheckRestartEligibilityResponse checkRestartEligibilityResponse)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(checkRestartEligibilityResponse.executionOutcome, Is.Not.Null.Or.Empty, "ExecutionOutcome Is Not Null Or Empty");
                Assert.That(checkRestartEligibilityResponse.data,             Is.Not.Null.Or.Empty, "Data Is Not Null Or Empty");
            }
            DocumentTemplate.DisplayBody("CheckRestartEligibilityResponse Object Properties Is Not Null Or Empty");
        }
        public CheckRestartEligibilityResponse populateCheckRestartEligibilityResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var checkRestartEligibilityResponse = new CheckRestartEligibilityResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new CheckRestartEligibilityData()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": checkRestartEligibilityResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": checkRestartEligibilityResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": checkRestartEligibilityResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "isEligibile": checkRestartEligibilityResponse.data.isEligibile = (bool)utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message": checkRestartEligibilityResponse.data.message = utilitiesHelper.ReadStringNullable(item.Value); break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: DocumentTemplate.DisplayFieldAndValue("Unknown property:", property.Name); break;
                }
            }
            return checkRestartEligibilityResponse;
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
