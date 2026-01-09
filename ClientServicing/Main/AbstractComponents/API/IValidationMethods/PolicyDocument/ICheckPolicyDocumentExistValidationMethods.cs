using ClientServicing.Main.Models.PolicyDocument;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.PolicyDocument
{
    public interface ICheckPolicyDocumentExistValidationMethods
    {
        public void ValidateObjectRequestDataIsNotNullOrEmptyOrLessThanZero(CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest);
        public void ValidateCheckPolicyDocumentExistResponseIsNotNullOrEmpty(CheckPolicyDocumentExistResponse checkPolicyDocumentExistResponse);
    }
}
