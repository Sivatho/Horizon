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
    public class InsertPolicyNoteValidationMethods : AbstractValidationMethods, IInsertPolicyNoteValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateInsertPolicyNoteRequest(InsertPolicyNoteRequest insertPolicyNoteRequest)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(insertPolicyNoteRequest.testYN,     Is.Not.Null.Or.Empty,               "TestYN Should Not Be Null Or Empty");
                Assert.That(insertPolicyNoteRequest.noteId,     Is.Not.LessThan(0),                 "NoteId Should Not Be Less Than Zero");
                Assert.That(insertPolicyNoteRequest.levelCd,    Is.Not.LessThan(0),                 "LevelCd Should Not Be Less Than Zero");
                Assert.That(insertPolicyNoteRequest.policyNo,   Is.Not.LessThan(0),                 "PolicyNo Should Not Be Less Than Zero");
                Assert.That(insertPolicyNoteRequest.entityNo,   Is.Not.LessThan(0),                 "EntityNo Should Not Be Less Than Zero");
                Assert.That(insertPolicyNoteRequest.noteText,   Is.Not.Null.Or.Empty,               "NoteText Should Not Be Null Or Empty");
                Assert.That(insertPolicyNoteRequest.effDate,    Is.Not.EqualTo(default(DateTime)),  "EffDate Should Not Equal To Defualt DateTime");
                Assert.That(insertPolicyNoteRequest.expDate,    Is.Not.EqualTo(default(DateTime)),  "ExpDate Should Not Equal To Defualt DateTime");
            }
            DocumentTemplate.DisplayBody("InsertPolicyNoteRequest String Properties Is Not Null Or Empty, Integer Properties Is Not Less Than Zero, DateTime Propertoes Is Not Equal To Defualt DateTime ");
        }

        public void ValidateInsertPolicyNoteResponse(InsertPolicyNoteResponse insertPolicyNoteResponse)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(insertPolicyNoteResponse.executionOutcome, Is.Not.Null.Or.Empty, "ExecutionOutcome Is Not Null Or Empty");
                Assert.That(insertPolicyNoteResponse.data, Is.Not.Null.Or.Empty, "Data Is Not Null Or Empty");
            }
            DocumentTemplate.DisplayBody("InsertPolicyNoteResponse Object Properties Is Not Null Or Empty");
        }

        public InsertPolicyNoteResponse populateInsertPolicyNoteResponse(RestResponse response) {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var insertPolicyNoteResponse = new InsertPolicyNoteResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   insertPolicyNoteResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     insertPolicyNoteResponse.executionOutcome.message =  utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      insertPolicyNoteResponse.executionOutcome.errors =   utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        insertPolicyNoteResponse.data =                      utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return insertPolicyNoteResponse;
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
