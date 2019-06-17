using Imgeneus.Core.Structures.Configuration;
using Imgeneus.Login.InternalServer;
using Imgeneus.Login.Packets;
using Imgeneus.Network.InternalServer;
using Imgeneus.Network.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Imgeneus.Login
{
    public sealed class LoginServer : Server<LoginClient>, ILoginServer
    {
        private readonly ILogger<LoginServer> logger;
        private readonly LoginConfiguration loginConfiguration;

        /// <summary>
        /// Gets the internal server instance.
        /// </summary>
        public ISServer InterServer { get; private set; }

        public static RSACryptoServiceProvider rsa;

        /// <summary>
        /// Gets the list of the connected worlds.
        /// </summary>
        public IEnumerable<WorldServerInfo> ClustersConnected => InterServer.WorldServers;

        public LoginServer(ILogger<LoginServer> logger, LoginConfiguration loginConfiguration)
            : base(new ServerConfiguration(loginConfiguration.Host, loginConfiguration.Port, loginConfiguration.MaximumNumberOfConnections))
        {
            this.logger = logger;
            this.loginConfiguration = loginConfiguration;
            this.InterServer = new ISServer(loginConfiguration.InterServerConfiguration);
        }

        protected override void OnStart()
        {
            this.logger.LogTrace("Host: {0}, Port: {1}, MaxNumberOfConnections: {2}",
            this.loginConfiguration.Host,
            this.loginConfiguration.Port,
            this.loginConfiguration.MaximumNumberOfConnections);
            this.InterServer.Start();
        }
        protected override void OnClientConnected(LoginClient client)
        {
            LoginPacketFactory.SendLoginHandshake(client);
        }

        protected override void OnError(Exception exception)
        {
            this.logger.LogInformation($"Socket error: {exception.Message}");
        }

        /// <inheritdoc />
        public IEnumerable<WorldServerInfo> GetConnectedWorlds() => this.InterServer.WorldServers;

        /// <inheritdoc />
        public LoginClient GetClientByUserID(int userID)
            => this.clients.Values.FirstOrDefault(x => x.IsConnected && x.UserID == userID);

        /// <inheritdoc />
        public bool IsClientConnected(int userID) => this.GetClientByUserID(userID) != null;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (InterServer != null)
            {
                InterServer.Stop();
                InterServer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
