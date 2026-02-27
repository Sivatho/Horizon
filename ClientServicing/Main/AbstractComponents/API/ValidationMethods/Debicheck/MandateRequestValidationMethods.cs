using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.EndPoints.Debicheck;
using ClientServicing.Main.Resources.Helper;
using Microsoft.CodeAnalysis.CSharp;
using RestSharp;
using static ClientServicing.Main.Models.Debicheck.MandatesRequest;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class MandateRequestValidationMethods : AbstractValidationMethods, IMandateRequestValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateMandateRequesDataIsNotNullOrEmpty(MandatesRequestData mandatesRequestData)
        {
            Assert.Multiple(() =>
            {
                foreach (MandatesRequest mandatesRequest in mandatesRequestData.listOfMandatesRequest!)
                {
                    Assert.That(mandatesRequest,                Is.Not.Null.Or.Not.Empty, "Response: MandatesRequest Should Be Null Or Not Empty");
                    Assert.That(mandatesRequest.policyNumber,   Is.Not.Null.Or.Not.Empty, "Response: Message Should Not Be Null Or Not Empty");
                    
                    if (mandatesRequest.policyNumber != null)
                    {                       
                        Assert.That(int.Parse(mandatesRequest.policyNumber!), Is.Not.LessThan(0), "Response: PolicyNumber Should Not Be Less Than 0");
                    }
                    
                    Assert.That(mandatesRequest.existingClient,             Is.True.Or.False,               "Response: Success Should True Or False");
                    Assert.That(mandatesRequest.payerMobileTelephoneNumber, Is.Null.Or.TypeOf<string>(),    "Response: PayerMobileTelephoneNumber Should Be Null Or Type Of String");
                    Assert.That(mandatesRequest.sourceSystemId,             Is.Not.LessThan(0),             "Response: SourceSystemId Should Not Be Less Than 0");
                    Assert.That(mandatesRequest.agentCode,                  Is.Null.Or.TypeOf<string>(),    "Response: agentCode Should Be Null Or Type Of String");
                    Assert.That(mandatesRequest.agentName,                  Is.Null.Or.TypeOf<string>(),    "Response: agentName Should Be Null Or Type Of String");
                    Assert.That(mandatesRequest.transactionType,            Is.Not.LessThan(0),             "Response: TransactionType Should Not Be Less Than 0");
                }
            });
            DocumentTemplate.DisplayBody("Validated: CheckStatusRequest Data Has Valid Properties and Values");

        }

        public void ValidateMandateResponseDataIsNotNullOrEmpty(MandatesRequestResponse mandaterequestresponse)
        {
            Assert.Multiple(() => {
                Assert.That(mandaterequestresponse,             Is.Not.Null,                "CheckStatusResponse Should Not Be Null");
                Assert.That(mandaterequestresponse.success,     Is.True.Or.False,           "Response: Success Should True Or False");
                Assert.That(mandaterequestresponse.didError,    Is.True.Or.False,           "Response: DidError Should True Or False");
                Assert.That(mandaterequestresponse.result,      Is.Not.Null.And.Not.Empty,  "Response: Result Should Not Be Null Or Empty");
                foreach (var result in mandaterequestresponse.result!)
                {
                    Assert.That(result.success,     Is.True.Or.False,               "Response: Success Should True Or False");
                    Assert.That(result.didError,    Is.True.Or.False,               "Response: Success Should True Or False");
                    Assert.That(result.message,     Is.Not.Null.Or.Empty,           "Response: Message Should Be Null Or Not Empty");
                    Assert.That(result.data,        Is.Null.Or.TypeOf<string>(),    "Response: Message Should Be Null Or Type Of String");
                }
            });
            DocumentTemplate.DisplayBody("Validated: CheckStatusRequest Data Has Valid Properties and Values");
        }
        public MandatesRequestResponse PopulateMandatesRequestResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content!);
            JsonElement root = jsonDocument.RootElement;

            var mandatesRequestResponse = new MandatesRequestResponse
            {
                result = new List<MandatesRequestResponseResult>()
            };
            foreach (var property in root.EnumerateObject()) {
                switch (property.Name)
                {
                    case "success": mandatesRequestResponse.success = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "didError": mandatesRequestResponse.didError = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "result":
                        var resultList = new List<MandatesRequestResponseResult>();
                        foreach (var resultItem in property.Value.EnumerateArray())
                        {
                            var mandatesRequestResponseResult = new MandatesRequestResponseResult();
                            foreach (var resultProperty in resultItem.EnumerateObject())
                            {
                                switch (resultProperty.Name)
                                {
                                    case "success":     mandatesRequestResponseResult.success =     (bool)utilitiesHelper.ReadBooleanNullable(resultProperty.Value)!; break;
                                    case "didError":    mandatesRequestResponseResult.didError =    (bool)utilitiesHelper.ReadBooleanNullable(resultProperty.Value)!; break;
                                    case "message":     mandatesRequestResponseResult.message =     utilitiesHelper.ReadStringNullable(resultProperty.Value)!; break;
                                    case "data":        mandatesRequestResponseResult.data =     utilitiesHelper.ReadStringNullable(resultProperty.Value)!; break;
                                    default: TestContext.Out.WriteLine($"Unknown property: {resultProperty.Name}"); break;

                                }
                            }
                            resultList.Add(mandatesRequestResponseResult); 
                        }
                        mandatesRequestResponse.result = resultList;
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;

                }
            }
            return mandatesRequestResponse;
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
