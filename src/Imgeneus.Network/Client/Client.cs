using Imgeneus.Network.Client.Internal;
using Imgeneus.Network.Common;
using Imgeneus.Network.Data;
using System;
using System.Net.Sockets;

namespace Imgeneus.Network.Client
{
    /// <summary>
    /// Provides a mechanism for creating managed TCP clients.
    /// </summary>
    public abstract class Client : Connection, IClient
    {
        private readonly ClientConnector connector;
        private ClientReceiver receiver;
        private ClientSender sender;

        /// <inheritdoc />
        public bool IsConnected => this.Socket.Connected;

        /// <inheritdoc />
        public bool IsRunning { get; private set; }

        /// <inheritdoc />
        public ClientConfiguration ClientConfiguration { get; protected set; }

        /// <summary>
        /// Creates a new <see cref="Client"/> instance.
        /// </summary>
        /// <param name="socketConnection"></param>
        protected Client() 
        {
            this.connector = new ClientConnector(this);
        }

        /// <inheritdoc />
        public void Connect()
        {
            if (this.IsRunning)
            {
                throw new InvalidOperationException("Client is already running.");
            }

            if (this.IsConnected)
            {
                throw new InvalidOperationException("Client is already connected to remote host.");
            }

            if (this.ClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(this.ClientConfiguration), "Undefined Client configuration.");
            }   

            if (this.ClientConfiguration.Port <= 0)
            {
                throw new ArgumentException($"Invalid port number '{this.ClientConfiguration.Port}' in configuration.", nameof(this.ClientConfiguration.Port));

            }

            if (NetworkHelper.BuildIPAddress(this.ClientConfiguration.Host) == null)
            {
                throw new ArgumentException($"Invalid host address '{this.ClientConfiguration.Host}' in configuration", nameof(this.ClientConfiguration.Host));
            }
                

            if (this.ClientConfiguration.BufferSize <= 0)
            {
                throw new ArgumentException($"Invalid buffer size '{this.ClientConfiguration.BufferSize}' in configuration.", nameof(this.ClientConfiguration.BufferSize));
            }

            this.sender = new ClientSender(this.CreateSocketEventArgs(null));
            this.receiver = new ClientReceiver(this);
            SocketAsyncEventArgs socketConnectEventArgs = this.CreateSocketEventArgs(null);
            socketConnectEventArgs.RemoteEndPoint = NetworkHelper.CreateIPEndPoint(this.ClientConfiguration.Host, this.ClientConfiguration.Port);

            SocketError errorCode = this.connector.Connect(socketConnectEventArgs);

            if (!this.IsConnected && errorCode != SocketError.Success)
            {
                this.OnSocketError(errorCode);
                return;
            }

            this.IsRunning = true;
            this.sender.Start();
        }

        /// <inheritdoc />
        public void Disconnect()
        {
            this.IsRunning = false;
            this.Socket.Disconnect(true);
            this.sender.Stop();
        }

        /// <inheritdoc />
        public abstract void HandlePacket(IPacketStream packet);

        /// <inheritdoc />
        public void SendPacket(IPacketStream packet) =>this.sender.AddPacketToQueue(new PacketData(this, packet.Buffer));
        /// <summary>
        /// Triggered when the client is connected to the remote end point.
        /// </summary>
        protected abstract void OnConnected();

        /// <summary>
        /// Triggered when the client is disconnected from the remote end point.
        /// </summary>
        protected abstract void OnDisconnected();

        /// <summary>
        /// Triggered when a error on the socket happend
        /// </summary>
        /// <param name="socketError"></param>
        protected abstract void OnSocketError(SocketError socketError);

        /// <summary>
        /// Creates a new <see cref="SocketAsyncEventArgs"/> for a <see cref="Client"/>.
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns></returns>
        private SocketAsyncEventArgs CreateSocketEventArgs(int? bufferSize)
        {
            var socketEvent = new SocketAsyncEventArgs()
            {
                UserToken = this
            };

            socketEvent.Completed += this.OnSocketCompleted;
            if (bufferSize.HasValue)
            {
                socketEvent.SetBuffer(new byte[bufferSize.Value], 0, bufferSize.Value);
            }

            return socketEvent;
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
                    case SocketAsyncOperation.Connect:
                        this.OnConnected();
                        this.connector.ReleaseConnectorLock();
                        break;
                    case SocketAsyncOperation.Receive:
                        this.receiver.Receive(e);
                        break;
                    case SocketAsyncOperation.Send:
                        this.sender.SendOperationCompleted(e);
                        break;
                    default: throw new InvalidOperationException("Unexpected SocketAsyncOperation.");
                }
            }
            catch
            {
                // TODO: catch exception and do something with it.
            }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            this.sender.Dispose();
            this.connector.Dispose();
            base.Dispose(disposing);
        }
    }
}
