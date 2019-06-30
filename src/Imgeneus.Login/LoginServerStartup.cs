using Imgeneus.Core;
using Imgeneus.Core.DependencyInjection;
using Imgeneus.Core.Helpers;
using Imgeneus.Core.Structures.Configuration;
using Imgeneus.Database;
using Imgeneus.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Imgeneus.Login
{
    public sealed class LoginServerStartup : IProgramStartup
    {
        private const string LoginConfigFile = "config/login.json";
        private const string DatabaseConfigFile = "config/database.json";

        /// <inheritdoc />
        public void Configure()
        {
            PacketHandler<LoginClient>.Initialize();

            var dbConfig = ConfigurationHelper.Load<DatabaseConfiguration>(DatabaseConfigFile);
            DependencyContainer.Instance
                .GetServiceCollection()
                .RegisterDatabaseServices(dbConfig);

            DependencyContainer.Instance.Register<ILoginServer, LoginServer>(ServiceLifetime.Singleton);
            DependencyContainer.Instance.Configure(services => services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning);
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            }));
            DependencyContainer.Instance.Configure(services =>
            {
                var loginConfiguration = ConfigurationHelper.Load<LoginConfiguration>(LoginConfigFile);
                services.AddSingleton(loginConfiguration);
            });
            DependencyContainer.Instance.BuildServiceProvider();
        }

        /// <inheritdoc />
        public void Run()
        {
            var logger = DependencyContainer.Instance.Resolve<ILogger<LoginServerStartup>>();
            var server = DependencyContainer.Instance.Resolve<ILoginServer>();
            try
            {
                logger.LogInformation("Starting LoginServer...");
                server.Start();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                logger.LogCritical(e, $"An unexpected error occured in LoginServer.");
#if DEBUG
                Console.ReadLine();
#endif
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DependencyContainer.Instance.Dispose();
            LogManager.Shutdown();
        }
    }
}
