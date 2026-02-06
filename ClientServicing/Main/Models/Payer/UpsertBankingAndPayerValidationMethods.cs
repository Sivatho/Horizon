using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Payer;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.Models.Payer
{
    public class UpsertBankingAndPayerValidationMethods : AbstractValidationMethods, IUpsertBankingAndPayerValidationMethods
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

        public void ValidateUpsertBankingAndPayerRequestIsNotNUllOrEmpty(UpsertBankingAndPayerRequest upsertBankingAndPayerRequest)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(upsertBankingAndPayerRequest, Is.Not.Null.Or.Empty, "upsertBankingAndPayerRequest Should Not Be Null Or Empty");
            }
            DocumentTemplate.DisplayBody("UpsertBankingAndPayerRequest Is Not Null Or Empty");
        }

        public void ValidateUpsertBankingAndPayerResponseIsNotNUllOrEmpt(InsertPolicyNoteResponse upsertBankingAndPayerResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(upsertBankingAndPayerResponse.executionOutcome, Is.Not.Null.Or.Empty, "Execution Should Not Be Null Or Empty");
                Assert.That(upsertBankingAndPayerResponse.data, Is.Not.Null.Or.Empty, "Data is Not Null Or Empty");
            }
            DocumentTemplate.DisplayBody("UpsertBankingAndPayerResponse Is Not Null Or Empty");
        }
        public InsertPolicyNoteResponse populateCheckPolicyIfMainMemberOnlyResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var upsertBankingAndPayerResponse = new InsertPolicyNoteResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": upsertBankingAndPayerResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": upsertBankingAndPayerResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": upsertBankingAndPayerResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data": upsertBankingAndPayerResponse.data = utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return upsertBankingAndPayerResponse;
        }
    }
}
