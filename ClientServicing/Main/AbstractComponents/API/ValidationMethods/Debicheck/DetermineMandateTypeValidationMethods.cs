using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using com.sun.xml.@internal.ws.api.message;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class DetermineMandateTypeValidationMethods : AbstractValidationMethods, IDetermineMandateTypeValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public void ValidateDetermineMandateTypeRequestDataIsNotNullOrEmpty(DetermineMandateTypeRequestData determineMandateTypeRequestData)
        {
            Assert.Multiple(() =>
            {
                Assert.That(determineMandateTypeRequestData,    Is.Not.Null, "CheckStatusRequest Should Not Be Null");
                foreach (DetermineMandateTypeRequest determineMandateTypeRequest in determineMandateTypeRequestData.listOfDetermineMandateTypeRequest!)
                {
                    Assert.That(determineMandateTypeRequest.policyNumber,                   Is.Not.Null.And.Not.Empty,  "Response: PolicyNumber Should Not Be Null Or Empty");
                    if (determineMandateTypeRequest.policyNumber != null)
                    {
                        Assert.That(int.Parse(determineMandateTypeRequest.policyNumber),    Is.Not.LessThan(0), "       Response: PolicyNumber as Integer Should Not Be Less Than 0");
                    }
                    Assert.That(determineMandateTypeRequest.sourceSystemId,                 Is.Not.LessThan(0),         "Response: SourceSystemId Should Not Be Less Than 0");
                    Assert.That(determineMandateTypeRequest.hasBankApp,                     Is.True.Or.False,           "Response: HasBankApp Should Be True Or False");
                }
            });
            DocumentTemplate.DisplayBody("Validated: DetermineMandateTypeRequest Data Has Valid Properties Values");

        }

        public void ValidateDetermineMandateTypeResponseDataIsNotNullOrEmpty(DetermineMandateTypeResponse determineMandateTypeResponse)
        {
            Assert.Multiple(() => {
                Assert.That(determineMandateTypeResponse,                           Is.Not.Null,            "DetermineMandateTypeResponse Should Not Be Null");
                Assert.That(determineMandateTypeResponse.success,                   Is.True.Or.False,       "Response: Success Should Be True Or False");
                Assert.That(determineMandateTypeResponse.result,                    Is.Not.Null,            "Response: Result Should Not Be Null");
                Assert.That(determineMandateTypeResponse.result.success,            Is.True.Or.False,       "Response: Result.Success Should Be True Or False");
                Assert.That(determineMandateTypeResponse.result.message,            Is.Not.Null.Or.Empty,   "Response: Result.Message Should Not Be Null Or Empty");
                Assert.That(determineMandateTypeResponse.result.data,               Is.Not.Null,            "Response: Data Should Be Not Null");
                Assert.That(determineMandateTypeResponse.result.data.mandateType,   Is.Not.Null,            "Response: Data.MandateType Should Be Not Null");
                var dataListCount = determineMandateTypeResponse.result.data.listOfStatusReason.Count;
                Assert.That(dataListCount,                                          Is.Not.LessThanOrEqualTo(0), "Response: Data.ListOfStatusReason Should Be Not Less Than or Equal To 0");
                foreach (var data in determineMandateTypeResponse.result.data.listOfStatusReason) {
                    Assert.That(data.message,                                       Is.Not.Null.And.TypeOf<string>(), "Response: Data.ListOfStatusReason.Message Should Not Be Nulll and Is Type Of String");
                    Assert.That(data.policyNumber,                                  Is.Not.Null.And.TypeOf<string>(), "Response: Data.ListOfStatusReason.PolicyNumber Should Not Be Nulll and Is Type Of String");
                }

            });
        }

        public DetermineMandateTypeResponse PopulateDetermineMandateTypeResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;

            var determineMandateTypeResponse = new DetermineMandateTypeResponse()
            {
                result = new SuccessBoolMessageStringDataObjectResult()
            };

            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "success": determineMandateTypeResponse.success = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "result":
                        var successBoolMessageStringDataObjectResult = new SuccessBoolMessageStringDataObjectResult();
                        foreach (var resultItem in property.Value.EnumerateObject())
                        {
                            switch (resultItem.Name) {
                                case "success": successBoolMessageStringDataObjectResult.success = (bool)utilitiesHelper.ReadBooleanNullable(resultItem.Value)!; break;
                                case "message": successBoolMessageStringDataObjectResult.message = utilitiesHelper.ReadStringNullable(resultItem.Value)!; break;
                                case "data":
                                    var mandateTypeStringStatusReasonObjectData = new MandateTypeStringStatusReasonObjectData() {
                                        listOfStatusReason = new List<statusReason>()
                                    };                                    
                                    foreach (var dataProperty in resultItem.Value.EnumerateObject())
                                    {
                                        switch (dataProperty.Name)
                                        {
                                            case "mandateType": mandateTypeStringStatusReasonObjectData.mandateType = utilitiesHelper.ReadStringNullable(dataProperty.Value)!; break;
                                            case "statusReason":
                                                var listOfStatusReason = new List<statusReason>();                                                
                                                foreach (var statusReasonPropertyArray in dataProperty.Value.EnumerateArray())
                                                {
                                                    var statusReason = new statusReason();
                                                    foreach (var statusReasonProperty in statusReasonPropertyArray.EnumerateObject()) {
                                                        switch (statusReasonProperty.Name) {
                                                            case "message": statusReason.message =  utilitiesHelper.ReadStringNullable(statusReasonProperty.Value)!; break;
                                                            case "policyNumber": statusReason.policyNumber =    utilitiesHelper.ReadStringNullable(statusReasonProperty.Value)!; break;
                                                            default: TestContext.Out.WriteLine($"Unknown property: {statusReasonProperty.Name}"); break;
                                                        }
                                                    }
                                                    listOfStatusReason.Add(statusReason);
                                                }
                                                mandateTypeStringStatusReasonObjectData.listOfStatusReason = listOfStatusReason;
                                                break;
                                            default: TestContext.Out.WriteLine($"Unknown property: {dataProperty.Name}"); break;
                                        }
                                    }
                                    successBoolMessageStringDataObjectResult.data = mandateTypeStringStatusReasonObjectData;
                                    break;
                                default: TestContext.Out.WriteLine($"Unknown property: {resultItem.Name}"); break;

                            }
                        }
                        determineMandateTypeResponse.result = successBoolMessageStringDataObjectResult; break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;                       
                }
            }
            return determineMandateTypeResponse;
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
