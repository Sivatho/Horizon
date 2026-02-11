using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class InsertPolicyNoteAPITests : InsertPolicyNoteValidationMethods
    {
        PolicyAPIClient PolicyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_InsertPolicyNoteRequestPayloadIsValid_When_InsertPolicyNoteAsyncThen_() {
            //Arrange
            var insertPolicyNoteRequest = JsonSerializer.Deserialize<InsertPolicyNoteRequest>(
                utilitiesHelper.ReadTestDataJson("Policy/Data", "InsertPolicyNoteRequestPayloadIsValid.json"));
            ValidateInsertPolicyNoteRequest(insertPolicyNoteRequest);
            
            //Act
            var response = await PolicyAPIClient.InsertPolicyNoteAsync(insertPolicyNoteRequest);
            var insertPolicyNoteResponse = populateInsertPolicyNoteResponse(response);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;

            //Assert            
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateInsertPolicyNoteResponse(insertPolicyNoteResponse);
        }
    }
}
