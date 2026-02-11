using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface ICheckPolicyIfMainMemberOnlyValidationMethods
    {
        public void ValidationCheckPolicyIfMainMemberOnlyRequest(PolicyNoRequest checkPolicyIfMainMemberOnlyRequest);
        public void ValidationCheckPolicyIfMainMemberOnlyResponse(InsertPolicyNoteResponse checkPolicyIfMainMemberOnlyResponse);
    }
}
