using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.Base;

namespace ClientServicing.Test.Tests.API
{
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
