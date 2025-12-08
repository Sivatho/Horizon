using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class JsonValidationRule
    {
        public string PropertyName { get; set; }
        public JsonValueKind[] AllowedKinds { get; set; }
        public bool IsRequired { get; set; } = true;
        public Dictionary<string, JsonValueKind[]> NestedRules { get; set; } // For arrays

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
