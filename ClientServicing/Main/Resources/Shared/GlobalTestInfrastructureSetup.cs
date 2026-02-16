using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

[SetUpFixture]  // no namespace => applies to the whole assembly
public class GlobalTestInfrastructureSetup
{
    public static IRestLibrary SharedRestLibrary { get; private set; } = null!;
    public static ServiceProvider ServiceProvider { get; private set; } = null!;
    private IDataAccess _dataAccess = null!;

    [OneTimeSetUp]
    public void BeforeAllTests()
    {
        ServiceProvider = ClientServicing.Main.Resources.Shared.ServiceSetup.BuilderServiceProvider();
        SharedRestLibrary = ServiceProvider.GetRequiredService<IRestLibrary>();
        _dataAccess = ServiceProvider.GetRequiredService<IDataAccess>();
    }

    [OneTimeTearDown]
    public void AfterAllTests()
    {
        (SharedRestLibrary as IDisposable)?.Dispose();
        ServiceProvider?.Dispose();
        GC.SuppressFinalize(this);
    }
}