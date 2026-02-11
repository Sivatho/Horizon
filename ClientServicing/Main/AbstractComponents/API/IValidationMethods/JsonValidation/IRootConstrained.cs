using System.Text.Json;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.JsonValidation
{
    public interface IRootConstrained
    {

        /// <summary>Allowed kinds for the root element (e.g., String, True, False, Object).</summary>
        ISet<JsonValueKind> AllowedRootKinds { get; }

        /// <summary>If root is a string, restrict to this enum (e.g., "true", "false"). Optional.</summary>
        ISet<string>? AllowedRootStringEnum { get; }

    }
}
