using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Payer;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Payer;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Payer
{
    public class GetPayerDetailsByPolicyNumberValidationMethods : AbstractValidationMethods, IGetPayerDetailsByPolicyNumberValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateGetPayerDetailsByPolicyNumberRequestIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(PolicyNoAndEffectiveDate getPayerDetailsByPolicyNumberRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(getPayerDetailsByPolicyNumberRequest.policyNo, Is.Not.LessThan(0), "PolicyNo Should Not Be Less Than Zero");
                Assert.That(getPayerDetailsByPolicyNumberRequest.effectiveDate, Is.Not.EqualTo(default(DateTime)), "EffectiveDate Should Not Equal To Default DateTime");
                Assert.That(getPayerDetailsByPolicyNumberRequest.auditToken, Is.Not.Null.Or.Empty, "AuditToken Should Not Be Null or Empty");
            }
            DocumentTemplate.DisplayBody("GetPayerDetailsByPolicyNumberRequest Is Not Less Than Zero, Is Not Equal To Default DateTime And Is Not Null or Empty");
        }

        public void ValidateGetPayerDetailsByPolicyNumberResponseIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(GetPayerDetailsByPolicyNumberResponse getPayerDetailsByPolicyNumberResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(getPayerDetailsByPolicyNumberResponse.executionOutcome, Is.Not.Null.Or.Empty, "ExecutionOutcome Should Not Be Null or Empty");
                Assert.That(getPayerDetailsByPolicyNumberResponse.data, Is.Not.Null.Or.Empty, "GetPayerDetailsByPolicyNumberResponse Should Not Be Null or Empty");
            }
            DocumentTemplate.DisplayBody("GetPayerDetailsByPolicyNumberResponse Should Not Be Null or Empty");
        }

        public GetPayerDetailsByPolicyNumberResponse populateGetPayerDetailsByPolicyNumberResponse(RestResponse restResponse)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(restResponse.Content);
            var getPayerDetailsByPolicyNumberResponse = new GetPayerDetailsByPolicyNumberResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new PayerDetailsByPolicyNumber()
                
            };
            getPayerDetailsByPolicyNumberResponse.data.gsd = new Models.Payer.GSD();
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": getPayerDetailsByPolicyNumberResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": getPayerDetailsByPolicyNumberResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": getPayerDetailsByPolicyNumberResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var dataItems in property.Value.EnumerateObject())
                        {
                            switch (dataItems.Name)
                            {
                                case "payerEntityNo":       getPayerDetailsByPolicyNumberResponse.data.payerEntityNo =      (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "policyNumber":        getPayerDetailsByPolicyNumberResponse.data.policyNumber =       utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "productCategory":     getPayerDetailsByPolicyNumberResponse.data.productCategory =    (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "productCategoryId":   getPayerDetailsByPolicyNumberResponse.data.productCategoryId =  (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "title":               getPayerDetailsByPolicyNumberResponse.data.title =              utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "titleCd":             getPayerDetailsByPolicyNumberResponse.data.titleCd =            (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "firstName":           getPayerDetailsByPolicyNumberResponse.data.firstName =          utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "surname":             getPayerDetailsByPolicyNumberResponse.data.surname =            utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "initials":            getPayerDetailsByPolicyNumberResponse.data.initials =           utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "emailAddress":        getPayerDetailsByPolicyNumberResponse.data.emailAddress =       utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "isAMember":           getPayerDetailsByPolicyNumberResponse.data.isAMember =          (bool)utilitiesHelper.ReadBooleanNullable(dataItems.Value); break;
                                case "legalRefNo":          getPayerDetailsByPolicyNumberResponse.data.legalRefNo =         utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "legalRefNoTypeCD":    getPayerDetailsByPolicyNumberResponse.data.legalRefNoTypeCD =   (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "dateOfBirth":         getPayerDetailsByPolicyNumberResponse.data.dateOfBirth =        (DateTime)utilitiesHelper.ReadDateTimeNullable(dataItems.Value); break;
                                case "genderCd":            getPayerDetailsByPolicyNumberResponse.data.genderCd =           (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "employerName":        getPayerDetailsByPolicyNumberResponse.data.employerName =       utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "employeeNumber":      getPayerDetailsByPolicyNumberResponse.data.employeeNumber =     utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "employeeDepartmentCd": getPayerDetailsByPolicyNumberResponse.data.employeeDepartmentCd = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "employeeDepartment":  getPayerDetailsByPolicyNumberResponse.data.employeeDepartment = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "authorizationTypeCd": getPayerDetailsByPolicyNumberResponse.data.authorizationTypeCd = (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "payroll":             getPayerDetailsByPolicyNumberResponse.data.payroll =            utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "mandateType":         getPayerDetailsByPolicyNumberResponse.data.mandateType =        utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "agentID":             getPayerDetailsByPolicyNumberResponse.data.agentID =            utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "isAuthorized":        getPayerDetailsByPolicyNumberResponse.data.isAuthorized =       (bool?)utilitiesHelper.ReadBooleanNullable(dataItems.Value); break;
                                case "homeNumber":          getPayerDetailsByPolicyNumberResponse.data.homeNumber =         utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "cellNumber":          getPayerDetailsByPolicyNumberResponse.data.cellNumber =         utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "workNumber":          getPayerDetailsByPolicyNumberResponse.data.workNumber =         utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "relationCd":          getPayerDetailsByPolicyNumberResponse.data.relationCd =         (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "bankId":              getPayerDetailsByPolicyNumberResponse.data.bankId =             (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "bankName":            getPayerDetailsByPolicyNumberResponse.data.bankName =           utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankShortName":       getPayerDetailsByPolicyNumberResponse.data.bankShortName =      utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "branchNo":            getPayerDetailsByPolicyNumberResponse.data.branchNo =           (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "branchCode":          getPayerDetailsByPolicyNumberResponse.data.branchCode =         utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankAccNo":           getPayerDetailsByPolicyNumberResponse.data.bankAccNo =          utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankAccHolderInitial": getPayerDetailsByPolicyNumberResponse.data.bankAccHolderInitial = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankAccHolderName":   getPayerDetailsByPolicyNumberResponse.data.bankAccHolderName = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankAccTypeCD":       getPayerDetailsByPolicyNumberResponse.data.bankAccTypeCD =      (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "bankAccTypeDesc":     getPayerDetailsByPolicyNumberResponse.data.bankAccTypeDesc =    utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "bankAccountID":       getPayerDetailsByPolicyNumberResponse.data.bankAccountID =      (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "bankAccSwiftCode":    getPayerDetailsByPolicyNumberResponse.data.bankAccSwiftCode =   utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "isActive":            getPayerDetailsByPolicyNumberResponse.data.isActive =           (bool?)utilitiesHelper.ReadBooleanNullable(dataItems.Value); break;
                                case "paymentTypeCD":       getPayerDetailsByPolicyNumberResponse.data.paymentTypeCD =      (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "paymentFreqCD":       getPayerDetailsByPolicyNumberResponse.data.paymentFreqCD =      (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "paymentRefId":        getPayerDetailsByPolicyNumberResponse.data.paymentRefId =       (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "premium":             getPayerDetailsByPolicyNumberResponse.data.premium =            (int)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "earlyTracking":       getPayerDetailsByPolicyNumberResponse.data.earlyTracking =      (bool)utilitiesHelper.ReadBooleanNullable(dataItems.Value); break;
                                case "debitDay":            getPayerDetailsByPolicyNumberResponse.data.debitDay =           (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "firstDebitDay":       getPayerDetailsByPolicyNumberResponse.data.firstDebitDay =      (DateTime?)utilitiesHelper.ReadDateTimeNullable(dataItems.Value); break;
                                case "firstDebitMonth":     getPayerDetailsByPolicyNumberResponse.data.firstDebitMonth =    utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "effectiveDate":       getPayerDetailsByPolicyNumberResponse.data.effectiveDate =      (DateTime?)utilitiesHelper.ReadDateTimeNullable(dataItems.Value); break;
                                case "gsd":
                                    foreach (var gsdItem in dataItems.Value.EnumerateObject())
                                    {
                                        switch (gsdItem.Name) {
                                            case "deductionAuthorization":  getPayerDetailsByPolicyNumberResponse.data.gsd.deductionAuthorization = (bool)utilitiesHelper.ReadBooleanNullable(gsdItem.Value); break;
                                            case "payrollName":             getPayerDetailsByPolicyNumberResponse.data.gsd.payrollName =            utilitiesHelper.ReadStringNullable(gsdItem.Value); break;
                                            case "payrollId":               getPayerDetailsByPolicyNumberResponse.data.gsd.payrollId =              (int?)utilitiesHelper.ReadInt32Nullable(gsdItem.Value); break;
                                            case "departmentId":            getPayerDetailsByPolicyNumberResponse.data.gsd.departmentId =           (int?)utilitiesHelper.ReadInt32Nullable(gsdItem.Value); break;
                                            case "departmentName":          getPayerDetailsByPolicyNumberResponse.data.gsd.departmentName =         utilitiesHelper.ReadStringNullable(gsdItem.Value); break;
                                            case "employeeNumber":          getPayerDetailsByPolicyNumberResponse.data.gsd.employeeNumber =         utilitiesHelper.ReadStringNullable(gsdItem.Value); break;
                                            case "mandateType":             getPayerDetailsByPolicyNumberResponse.data.gsd.mandateType =            utilitiesHelper.ReadStringNullable(gsdItem.Value); break;
                                            default: DocumentTemplate.DisplayFieldAndValue("Unknown property in data", dataItems.Name); break;
                                        }
                                    }
                                    break;
                                case "lastChanged":             getPayerDetailsByPolicyNumberResponse.data.lastChanged =    utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "userID":                  getPayerDetailsByPolicyNumberResponse.data.userID =         (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "entityNo":                getPayerDetailsByPolicyNumberResponse.data.entityNo =       (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "agentCode":               getPayerDetailsByPolicyNumberResponse.data.agentCode =      (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "agentName":               getPayerDetailsByPolicyNumberResponse.data.agentName =      utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "payAtNumber":             getPayerDetailsByPolicyNumberResponse.data.payAtNumber =    utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "csdEmployeeNo":           getPayerDetailsByPolicyNumberResponse.data.csdEmployeeNo =  (int?)utilitiesHelper.ReadInt32Nullable(dataItems.Value); break;
                                case "csdCompanyDepartment":    getPayerDetailsByPolicyNumberResponse.data.csdCompanyDepartment = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                                case "csdCompanyName":          getPayerDetailsByPolicyNumberResponse.data.csdCompanyName = utilitiesHelper.ReadStringNullable(dataItems.Value); break;
                            }
                        }
                        break;
                    default:DocumentTemplate.DisplayFieldAndValue("Unknown property", property.Name); break;
                }
            }
            return getPayerDetailsByPolicyNumberResponse;
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
