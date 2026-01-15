using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.Helper
{
    public class JsonValidationRule
    {
        // These two must always be provided when creating a rule
        public required string PropertyName { get; set; }
        public required JsonValueKind[] AllowedKinds { get; set; }

        public bool IsRequired { get; set; } = true;

        // These may be absent (null) for many rules, keep them nullable
        public Dictionary<string, JsonValueKind[]>? NestedRules { get; set; }
        public Dictionary<string, JsonValueKind[]>? ArrayItemRules { get; internal set; }

        public static void ValidateJson(JsonElement root, List<JsonValidationRule> rules)
        {
            foreach (var rule in rules)
            {
                if (!root.TryGetProperty(rule.PropertyName, out var property))
                {
                    if (rule.IsRequired)
                        Assert.Fail($"Property '{rule.PropertyName}' is missing.");
                    continue;
                }

                Assert.That(rule.AllowedKinds.Contains(property.ValueKind),
                    $"Property '{rule.PropertyName}' has invalid type. Expected: {string.Join(", ", rule.AllowedKinds)}");

                // Validate nested array items
                if (property.ValueKind == JsonValueKind.Array && rule.NestedRules != null)
                {
                    foreach (var item in property.EnumerateArray())
                    {
                        foreach (var nestedRule in rule.NestedRules)
                        {
                            Assert.That(item.TryGetProperty(nestedRule.Key, out var nestedProp), Is.True,
                                $"Missing property '{nestedRule.Key}' in array item");
                            Assert.That(nestedRule.Value.Contains(nestedProp.ValueKind),
                                $"Property '{nestedRule.Key}' has invalid type");
                        }
                    }
                }
            }
        }
    }
}
