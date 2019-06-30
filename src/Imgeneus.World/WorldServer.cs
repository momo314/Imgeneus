using Imgeneus.Core.Structures.Configuration;
using Imgeneus.Network.Server;
using Imgeneus.World.InternalServer;
using Microsoft.Extensions.Logging;
using System;

namespace Imgeneus.World
{
    public sealed class WorldServer : Server<WorldClient>, IWorldServer
    {
        private readonly ILogger<WorldServer> logger;
        private readonly WorldConfiguration worldConfiguration;

        /// <summary>
        /// Gets the Inter-Server client.
        /// </summary>
        public ISClient InterClient { get; private set; }

        public WorldServer(ILogger<WorldServer> logger, WorldConfiguration configuration) 
            : base(new ServerConfiguration(configuration.Host, configuration.Port, configuration.MaximumNumberOfConnections))
        {
            this.logger = logger;
            this.worldConfiguration = configuration;
            this.InterClient = new ISClient(configuration);
        }

        protected override void OnStart()
        {
            this.logger.LogTrace("Host: {0}, Port: {1}, MaxNumberOfConnections: {2}",
                this.worldConfiguration.Host,
                this.worldConfiguration.Port,
                this.worldConfiguration.MaximumNumberOfConnections);
            this.InterClient.Connect();
        }

        /// <inheritdoc />
        protected override void OnError(Exception exception)
        {
            this.logger.LogInformation($"World Server error: {exception.Message}");
        }
    }
}
