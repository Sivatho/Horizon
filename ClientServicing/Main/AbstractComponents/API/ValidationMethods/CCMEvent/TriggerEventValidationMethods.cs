using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.CCMEvent;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.CCMEvent;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.CCMEvent
{
    public class TriggerEventValidationMethods : AbstractValidationMethods, ITriggerEventValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateTriggerEventRequestPayload(TriggerEventRequest triggerEventRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(triggerEventRequest.policyNo,       Is.Not.LessThan(0),             "Response: PolicyNo Should Not Be Less Than 0");
                Assert.That(triggerEventRequest.legacyPolicyNo, Is.Null.Or.TypeOf<string>(),    "Response: legacyPolicyNo Should Be NUll or Type of String");
                Assert.That(triggerEventRequest.partnerCd,      Is.Not.LessThan(0),             "Response: PartnerCd Should Not Be Less Than 0");
                Assert.That(triggerEventRequest.eventTypeCd,    Is.Not.LessThan(0),             "Response: EventTypeCd Should Not Be Less Than 0");
                Assert.That(triggerEventRequest.eventTypeDesc,  Is.Null.Or.TypeOf<string>(),    "Response: EventTypeDesc Should Be NUll or Type of String");
                Assert.That(triggerEventRequest.quoteId,        Is.Not.LessThan(0),             "Response: QuoteId Should Not Be Less Than 0");
                Assert.That(triggerEventRequest.effectiveDate,  Is.Not.EqualTo(default(DateTime)), "Response: PolicyNo Should Not Equal To Default DateTime");
                Assert.That(triggerEventRequest.userId, Is.Null.Or.TypeOf<string>(),            "Response: UserId Should Be NUll or Type of String");
            });
            DocumentTemplate.DisplayBody("Validate: TriggerEventRequest Data Has Valid Properties and Values");
        }

        public void ValidateTriggerEventResponsePayload(TriggerEventResponse triggerEventResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(triggerEventResponse.executionOutcome,              Is.Not.Null.Or.Empty, "Response: ExecutionOutcome Should Not Be Null Or Empty");
                Assert.That(triggerEventResponse.data,                          Is.Not.Null.Or.Empty, "Response: Data should not be null");

                Assert.That(triggerEventResponse.executionOutcome.succeeded,    Is.True.Or.False,               "Response: ExecutionOutcome.Succeeded Should Be True Or False");
                Assert.That(triggerEventResponse.executionOutcome.message,      Is.Null.Or.TypeOf<string>(),    "Response: ExecutionOutcome.Message Should Be NUll or Type of String");
                Assert.That(triggerEventResponse.executionOutcome.errors,       Is.Null.Or.TypeOf<string>(),    "Response: ExecutionOutcome.Errors Should Be NUll or Type of String");

                Assert.That(triggerEventResponse.data.token,                    Is.Not.Null.Or.Empty,           "Response: Data.Token Should Not Be Null Or Empty");
                Assert.That(triggerEventResponse.data.message,                  Is.Not.Null.Or.Empty,           "Response: Data.Message Should Not Be null Or Empty");
                Assert.That(triggerEventResponse.data.success == true || triggerEventResponse.data.success == false, "Response: Data.Success Should Be True Or False");
            });
        }

        public TriggerEventResponse PopulateTriggerEventResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;
            var triggerEventResponse = new TriggerEventResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new TriggerEvent()
            };
            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   triggerEventResponse.executionOutcome.succeeded =   (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "message":     triggerEventResponse.executionOutcome.message =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      triggerEventResponse.executionOutcome.errors =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "token":   triggerEventResponse.data.token =       utilitiesHelper.ReadStringNullable(item.Value)!; break;
                                case "success": triggerEventResponse.data.success =     (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message": triggerEventResponse.data.message =       utilitiesHelper.ReadStringNullable(item.Value)!; break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property in data: {property.Name}"); break;
                }
            }
            return triggerEventResponse;
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
