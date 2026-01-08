using ClientServicing.Main.Models.PolicyDocument;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.PolicyDocument
{
    public interface ICheckPolicyDocumentExistValidationMethods
    {
        public void ValidateCheckPolicyDocumentExistRequestIsNotNullOrEmpty(CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest);
        public void ValidateCheckPolicyDocumentExistResponseIsNotNullOrEmpty(CheckPolicyDocumentExistResponse checkPolicyDocumentExistResponse);
    }
}
