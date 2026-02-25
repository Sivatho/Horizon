using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.CCMEvent;
using ClientServicing.Main.Models.CCMEvent;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.CCMEvent
{
    public class GetEventDetailConstructBPEValidationMethods : AbstractValidationMethods, IGetEventDetailConstructBPEValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public void ValidateGetEventDetailConstructBPERequestPayload(GetEventDetailConstructBPERequest getEventDetailConstructBPERequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getEventDetailConstructBPERequest,          Is.Not.Null,        "Request payload should not be null");
                Assert.That(getEventDetailConstructBPERequest.policyNo, Is.Not.LessThan(0), "Request: PolicyNo Should Not Be Less Than 0");
                Assert.That(getEventDetailConstructBPERequest.eventTypeCd, Is.Not.LessThan(0), "Request: EventTypeCd Should Not Be Less Than 0");
                    Assert.That(getEventDetailConstructBPERequest.effectiveDate, Is.Not.EqualTo(default(DateTime)), "Request: EffectiveDate Should Not Be Default DateTime");
            });
        }

        public void ValidateGetEventDetailConstructBPEResponsePayload(GetEventDetailConstructBPEResponse getEventDetailConstructBPEResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getEventDetailConstructBPEResponse.executionOutcome, Is.Not.Null.Or.Empty, "Response: ExecutionOutcome Should Not Be Null Or Empty");
                Assert.That(getEventDetailConstructBPEResponse.data, Is.Not.Null.Or.Empty, "Response: Data should not be null");

                Assert.That(getEventDetailConstructBPEResponse.executionOutcome.succeeded, Is.True.Or.False, "Response: ExecutionOutcome.Succeeded Should Be True Or False");
                Assert.That(getEventDetailConstructBPEResponse.executionOutcome.message, Is.Null.Or.TypeOf<string>(), "Response: ExecutionOutcome.Message Should Be NUll or Type of String");
                Assert.That(getEventDetailConstructBPEResponse.executionOutcome.errors, Is.Null.Or.TypeOf<string>(), "Response: ExecutionOutcome.Errors Should Be NUll or Type of String");

                Assert.That(getEventDetailConstructBPEResponse.data.jsonData, Is.Not.Null.Or.Empty, "Response: Data.JsonData Should Not Be Null Or Empty");
                Assert.That(getEventDetailConstructBPEResponse.data.message, Is.Not.Null.Or.Empty, "Response: Data.Message Should Not Be null Or Empty");
                Assert.That(getEventDetailConstructBPEResponse.data.success == true || getEventDetailConstructBPEResponse.data.success == false, "Response: Data.Success Should Be True Or False");
            });
        }
        public GetEventDetailConstructBPEResponse PopulateGetEventDetailConstructBPEResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;
            var getEventDetailConstructBPEResponse = new GetEventDetailConstructBPEResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new GetEventDetailConstructBPE()
            };
            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   getEventDetailConstructBPEResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "message":     getEventDetailConstructBPEResponse.executionOutcome.message =   utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      getEventDetailConstructBPEResponse.executionOutcome.errors =    utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "jsonData":    getEventDetailConstructBPEResponse.data.jsonData =  utilitiesHelper.ReadStringNullable(item.Value)!; break;
                                case "success":     getEventDetailConstructBPEResponse.data.success =   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message":     getEventDetailConstructBPEResponse.data.message =   utilitiesHelper.ReadStringNullable(item.Value)!; break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return getEventDetailConstructBPEResponse;
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
