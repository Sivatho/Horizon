using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.DataAccess.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Main.Resources.Shared
{
    public static class ServiceSetup
    {
        public static ServiceProvider BuilderServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {
                        "ConnectionStrings:horizonDbConnectionString",
                        Environment.GetEnvironmentVariable("HORIZON_DB_CONNECTION_STRING") 
                        ?? "Server=TV4-POLSQLAG-01; Database=Polly_C; integrated security=True;MultipleActiveResultSets=True; Trusted_Connection=True; TrustServerCertificate=True;"
                    }
                })
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var conString = "horizonDbConnectionString";
            var connectionString = configuration.GetConnectionString(conString);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException($"Connection String '{conString}' is missing from configuration");

            var services = new ServiceCollection();
            services.AddSingleton<IDataAccess>(sp => new MsSqlDataAccess(connectionString));
            services.AddSingleton<IRestLibrary, RestLibrary>();

            return services.BuildServiceProvider();
        }
    }
}