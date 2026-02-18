using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.DataAccess.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ClientServicing.Main.Resources.Shared
{
    public static class ServiceSetup
    {
        public static ServiceProvider BuilderServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddUserSecrets<ServiceSetupMarker>(optional: true)  // Use marker class instead
                .AddEnvironmentVariables()                           // For CI/CD and production
                .Build();

            var conString = "horizonDbConnectionString";
            var connectionString = configuration.GetConnectionString(conString);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = BuildWindowsAuthConnectionString();
                throw new InvalidOperationException(
                    $"Connection String '{conString}' is missing. " +
                    $"Set via User Secrets, environment variables, or appsettings.json");
            }

            var services = new ServiceCollection();
            services.AddSingleton<IDataAccess>(sp => new MsSqlDataAccess(connectionString));
            services.AddSingleton<IRestLibrary, RestLibrary>();

            return services.BuildServiceProvider();
        }
        /// <summary>
        /// Builds a Windows Authentication connection string as fallback.
        /// Uses the current Windows identity to authenticate with SQL Server.
        /// </summary>
        private static string BuildWindowsAuthConnectionString()
        {
            return "Server=TV4-POLSQLAG-01; Database=Polly_C; Integrated Security=true; " +
                   "MultipleActiveResultSets=True; TrustServerCertificate=True; Encrypt=false;";
        }
    }

    /// <summary>
    /// Marker class used to identify the assembly for User Secrets configuration.
    /// This allows AddUserSecrets<T>() to work correctly (requires a non-static type).
    /// </summary>
    public class ServiceSetupMarker { }
}