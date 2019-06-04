using Imgeneus.Network.Common;
using System;
using System.Collections.Generic;

namespace Imgeneus.Network.Server
{
    public interface IServer : IConnection
    {
        event EventHandler Started;
        event EventHandler Stopped;
        event EventHandler<Exception> Error;
        event EventHandler<IServerClient> ClientConnected;
        event EventHandler<IServerClient> ClientDisconnected;

        /// <summary>
        /// Gets the server's internal configuration.
        /// </summary>
        ServerConfiguration ServerConfiguration { get; }

        /// <summary>
        /// Gets a value that indicates if the server is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets a connected client by his id.
        /// </summary>
        /// <param name="clientId">The client id</param>
        /// <returns>The client matching the given Id.</returns>
        IServerClient GetClient(Guid clientId);

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        void Start();

        /// <summary>
        /// Stops the server.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        void Stop();

        /// <summary>
        /// Disconnects a client.
        /// </summary>
        /// <param name="client">The client to disconnect.</param>
        void DisconnectClient(IServerClient client);

        /// <summary>
        /// Disconnects a client by his id.
        /// </summary>
        /// <param name="clientId">The client id to disconnect.</param>
        void DisconnectClient(Guid clientId);

        /// <summary>
        /// Sends a message to a connected client.
        /// </summary>
        /// <param name="client">The client to send packet.</param>
        /// <param name="packetData">The packet data to send.</param>
        void SendPacketTo(IServerClient client, byte[] packetData);

        /// <summary>
        /// Sends a message to a collection of clients.
        /// </summary>
        /// <param name="clients">The clients collection.</param>
        /// <param name="packetData">The packet data to send.</param>
        void SendPacketTo(IEnumerable<IServerClient> clients, byte[] packetData);

        /// <summary>
        /// Sends a message to every connected clients.
        /// </summary>
        /// <param name="packetData">The packet data to send.</param>
        void SendPacketToAll(byte[] packetData);
    }
}
