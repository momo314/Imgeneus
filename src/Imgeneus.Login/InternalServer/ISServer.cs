using Imgeneus.Core.DependencyInjection;
using Imgeneus.Core.Structures.Configuration;
using Imgeneus.Network;
using Imgeneus.Network.InternalServer;
using Imgeneus.Network.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imgeneus.Login.InternalServer
{
    public sealed class ISServer : Server<ISClient>
    {
        private readonly ILogger<ISServer> logger;

        public ISServer(InterServerConfiguration configuration) : base(configuration.Host, configuration.Port, 5)
        {
            this.logger = DependencyContainer.Instance.Resolve<ILogger<ISServer>>();
        }

        protected override void OnStart()
        {
            PacketHandler<ISClient>.Initialize();
            logger.LogInformation("ISC server is started and listen on {0}:{1}.",
            this.ServerConfiguration.Host,
            this.ServerConfiguration.Port);
            this.logger.LogTrace("Inter-Server -> Host: {0}, Port: {1}, MaxNumberOfConnections: {2}",
            this.ServerConfiguration.Host,
            this.ServerConfiguration.Port,
            this.ServerConfiguration.MaximumNumberOfConnections);
        }

        protected override void OnClientConnected(ISClient client)
        {
            this.logger.LogTrace("New world server connected from {0}", client.RemoteEndPoint);
        }

        protected override void OnClientDisconnected(ISClient client)
        {
            this.logger.LogTrace("World server disconnected");
        }

        protected override void OnError(Exception exception)
        {
            this.logger.LogInformation($"Internal-Server socket error: {exception.Message}");
        }

        /// <summary>
        /// Gets the list of the connected worlds.
        /// </summary>
        public IEnumerable<WorldServerInfo> WorldServers => this.clients.Values.Select(x => x.WorldServerInfo);

    }
}
