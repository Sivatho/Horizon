using ClientServicing.Main.AbstractComponents.API.Base;

namespace ClientServicing.Test.Tests.API
{


    /// <summary>
    /// Method Name: GlobalRestLibrarySetup
    /// Description:
    ///     Provides a single, shared instance of RestLibrary for all tests
    ///     within the namespace. This setup fixture runs once before any test
    ///     executes, and disposes of the shared RestClient after all tests
    ///     have completed. It is used to centralize API client initialization
    ///     and resource cleanup across the entire test suite.
    /// Advantage:
    ///     - Ensures all tests use a consistent RestClient instance.
    ///     - Reduces overhead by initializing RestLibrary only once instead
    ///       of per test or per fixture.
    ///     - Improves test performance by reusing underlying HTTP resources.
    ///     - Provides a clean, controlled place for global test setup/teardown.
    /// Disadvantage:
    ///     - Shared state can introduce test inter-dependencies if tests
    ///       modify the RestClient configuration.
    ///     - Tests cannot run in full parallel isolation because they share
    ///       the same RestClient instance.
    ///     - Requires careful cleanup to avoid resource leaks.
    /// </summary>

    [SetUpFixture]
    public class GlobalRestLibrarySetup
    {
        public static IRestLibrary SharedRestLibrary { get; private set; }
        
        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            SharedRestLibrary = new RestLibrary();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            SharedRestLibrary.RestClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
