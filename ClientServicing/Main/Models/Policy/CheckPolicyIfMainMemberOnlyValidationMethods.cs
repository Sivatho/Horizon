using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.Models.Policy
{
    public class CheckPolicyIfMainMemberOnlyValidationMethods : AbstractValidationMethods, ICheckPolicyIfMainMemberOnlyValidationMethods
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

        public void ValidationCheckPolicyIfMainMemberOnlyRequest(PolicyNoRequest checkPolicyIfMainMemberOnlyRequest)
        {
            Assert.That(checkPolicyIfMainMemberOnlyRequest.policyNo, Is.Not.LessThan(0), "PolicyNo Should Not Be Less Than Zero");
        }

        public void ValidationCheckPolicyIfMainMemberOnlyResponse(InsertPolicyNoteResponse checkPolicyIfMainMemberOnlyResponse)
        {
            using(Assert.EnterMultipleScope()) {
                Assert.That(checkPolicyIfMainMemberOnlyResponse.executionOutcome,   Is.Not.Null.Or.Empty, "ExecutionOutcome Is Not Null Or Empty");
                Assert.That(checkPolicyIfMainMemberOnlyResponse.data,               Is.Not.Null.Or.Empty, "Data Is Not Null Or Empty");
            }
            DocumentTemplate.DisplayBody("CheckPolicyIfMainMemberOnlyResponse Object Properties Is Not Null Or Empty");
        }
        public InsertPolicyNoteResponse populateCheckPolicyIfMainMemberOnlyResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var checkPolicyIfMainMemberOnlyResponse = new InsertPolicyNoteResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   checkPolicyIfMainMemberOnlyResponse.executionOutcome.succeeded =    (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     checkPolicyIfMainMemberOnlyResponse.executionOutcome.message =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      checkPolicyIfMainMemberOnlyResponse.executionOutcome.errors =       utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        checkPolicyIfMainMemberOnlyResponse.data =                          utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return checkPolicyIfMainMemberOnlyResponse;
        }

    }
}
