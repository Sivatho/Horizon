using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FSharp.Json;

namespace ClientServicing.Main.Resources.Helper
{
    public class JsonDataLoader
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        /// <summary>
        /// Loads and deserializes JSON test data into a strongly typed collection.
        /// 
        /// <para><b>Description:</b></para>
        /// Reads a JSON file from the specified test data folder, deserializes the content
        /// into a list of objects of type <typeparamref name="T"/>, and returns the result.
        /// This helper is typically used in automated testing to supply structured test data.
        /// 
        /// <para><b>Benefits:</b></para>
        /// - Simplifies loading and deserializing JSON-based test data  
        /// - Ensures case-insensitive property mapping for resilience  
        /// - Avoids null reference issues by always returning an initialized list  
        /// - Allows reusable, type-safe data loading across multiple test scenarios  
        /// </summary>
        /// <typeparam name="T">The object type to deserialize each JSON item into.</typeparam>
        /// <param name="parentFolderName">The name of the parent folder where the JSON test file is stored.</param>
        /// <param name="fileName">The JSON file name including extension.</param>
        /// <returns>A collection of deserialized objects of type <typeparamref name="T"/>.</returns>
        public IEnumerable<T> ReadJsonTestDataList<T>(string parentFolderName, string fileName) where T : class
        {
            string jsonString = utilitiesHelper.ReadTestDataJson(parentFolderName, fileName);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(jsonString, options) ?? new List<T>();
        }

        /// <summary>
        /// Reads a JSON test data file and converts each root-level field into an NUnit TestCaseData entry.
        ///
        /// <para><b>Description:</b></para>
        /// This method loads a JSON object from the specified test data file, deserializes it into a
        /// dictionary, and generates a collection of <see cref="TestCaseData"/> objects. Each key/value pair
        /// from the JSON object becomes an individual test case, allowing parameterized NUnit tests to run
        /// dynamically based on external test data. This supports data-driven testing by mapping JSON fields
        /// directly into test method parameters.
        ///
        /// <para><b>Benefits:</b></para>
        /// • Enables true data-driven test execution using values directly from external JSON files.<br/>
        /// • Reduces duplicated test code by allowing multiple scenarios to run from a single test method.<br/>
        /// • Makes test data easier to maintain, modify, and extend without editing C# code.<br/>
        /// • Provides descriptive test names using <see cref="TestCaseData.SetName(string)"/> for clear reporting.<br/>
        /// • Supports flexible test design by passing both the key and value of each JSON field as parameters.<br/>
        ///
        /// <para><b>Parameters:</b></para>
        /// <param name="parentFolderName">The folder where the JSON test data file is located.</param>
        /// <param name="fileNameAndExt">The name of the JSON file, including its extension.</param>
        ///
        /// <para><b>Returns:</b></para>
        /// <returns>
        /// An enumerable collection of <see cref="TestCaseData"/> objects, where each entry represents a
        /// single JSON field mapped to a test case. Each test case includes the field name and value as
        /// parameters and uses a dynamically constructed test name for clarity.
        /// </returns>
        /// </summary>
        public IEnumerable<TestCaseData> ReadJsonTestDataFields(string parentFolderName, string fileNameAndExt)
        {
            string jsonString = utilitiesHelper.ReadTestDataJson(parentFolderName, fileNameAndExt);

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString)!;

            foreach (var kvp in dict)
            {   
                yield return new TestCaseData(kvp.Key, kvp.Value)
                    .SetName($"Given_{kvp.Key}Is{kvp.Value}_And_RequestPayloadisValid_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid");
            }
        }
    }
}
