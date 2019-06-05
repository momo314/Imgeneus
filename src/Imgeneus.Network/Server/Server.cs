using Imgeneus.Network.Common;
using Imgeneus.Network.Server.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Imgeneus.Network.Server
{
    /// <summary>
    /// Provides a basic mechanism to start a TCP server.
    /// </summary>
    public class Server<T> : Connection, IServer where T : class, IServerClient
    {
        private readonly ConcurrentDictionary<Guid, T> clients;
        private readonly BufferManager bufferManager;
        private readonly ServerAcceptor<T> acceptor;

        /// <inheritdoc />
        public event EventHandler Started;

        /// <inheritdoc />
        public event EventHandler Stopped;

        /// <inheritdoc />
        public event EventHandler<Exception> Error;

        /// <inheritdoc />
        public event EventHandler<IServerClient> ClientConnected;

        /// <inheritdoc />
        public event EventHandler<IServerClient> ClientDisconnected;

        /// <inheritdoc />
        public ServerConfiguration ServerConfiguration { get; }

        /// <inheritdoc />
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Crestes a default <see cref="Server{T}"/> instance.
        /// </summary>
        /// <param name="host">The server host.</param>
        /// <param name="port">The server listening port.</param>
        public Server(string host, int port)
            : this(new ServerConfiguration(host, port))
        {

        }

        /// <summary>
        /// Creates a new <see cref="Server{T}"/> using <see cref="Server.ServerConfiguration"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="Server.ServerConfiguration"/>.</param>
        public Server(ServerConfiguration configuration)
            : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            this.ServerConfiguration = configuration;
            this.clients = new ConcurrentDictionary<Guid, T>();
            this.acceptor = new ServerAcceptor<T>(this);
            this.bufferManager = new BufferManager(configuration.MaximumNumberOfConnections, configuration.ClientBufferSize);
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
                throw new InvalidOperationException("Server is already running.");

            this.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.Socket.Bind(NetworkHelper.CreateIPEndPoint(this.ServerConfiguration.Host, this.ServerConfiguration.Port));
            this.Socket.Listen(this.ServerConfiguration.Backlog);

            this.IsRunning = true;
            this.OnStart();
            this.acceptor.StartAccept();

        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            if (!this.IsRunning)
                throw new InvalidOperationException("Server is not running.");

            this.Socket.Close();
            this.OnStop();
        }

        /// <inheritdoc />
        public IServerClient GetClient(Guid clientId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void DisconnectClient(IServerClient client)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void DisconnectClient(Guid clientId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SendPacketTo(IServerClient client, byte[] packetData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SendPacketTo(IEnumerable<IServerClient> clients, byte[] packetData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SendPacketToAll(byte[] packetData)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnStart()
        {
            this.Started?.Invoke(this, null);
        }

        protected virtual void OnStop()
        {
            this.Stopped?.Invoke(this, null);
        }

        protected virtual void OnError(Exception exception)
        {
            this.Error?.Invoke(this, exception);
        }

        protected virtual void OnClientConnected(T client)
        {
            this.ClientConnected?.Invoke(this, client);
        }

        protected virtual void OnClientDisconnected(T client)
        {
            this.ClientDisconnected?.Invoke(this, client);
        }

        /// <summary>
        /// Method called when a <see cref="SocketAsyncEventArgs"/> completes an async operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnSocketCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Accept:
                        break;
                    case SocketAsyncOperation.Receive:
                        break;
                    case SocketAsyncOperation.Send:
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected SocketAsyncOperation.");
                }
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }

        /// <summary>
        /// Creates a new accepted client instance.
        /// </summary>
        /// <param name="acceptedSocketEvent"></param>
        internal void CreateClient(SocketAsyncEventArgs acceptedSocketEvent)
        {
            throw new NotImplementedException();
        }

    }
}
