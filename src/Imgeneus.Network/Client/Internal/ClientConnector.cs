using System.Net.Sockets;
using System.Threading;

namespace Imgeneus.Network.Client.Internal
{
    internal sealed class ClientConnector
    {
        private readonly Client client;
        private readonly AutoResetEvent autoConnectEvent;

        /// <summary>
        /// Creates a new <see cref="ClientConnector"/> instance.
        /// </summary>
        /// <param name="client">The client to using the connector.</param>
        public ClientConnector(Client client)
        {
            this.client = client;
            this.autoConnectEvent = new AutoResetEvent(false);
        }

        /// <summary>
        /// Connects to the remote host.
        /// </summary>
        /// <param name="connectSocketEvent">The connect event.</param>
        /// <returns>The socket error code.</returns>
        public SocketError Connect(SocketAsyncEventArgs connectSocketEvent)
        {
            SocketError error = this.InternalConnect(connectSocketEvent);

            if (!this.client.IsConnected)
            {
                if (this.client.ClientConfiguration.RetryOptions == ClientRetryOptions.Infinite)
                {
                    while (!this.client.IsConnected)
                    {
                        error = this.InternalConnect(connectSocketEvent);
                    }
                }
                else if (this.client.ClientConfiguration.RetryOptions == ClientRetryOptions.Limited)
                {
                    for (int i = 0; i < this.client.ClientConfiguration.MaxAttempts && !this.client.IsConnected; i++)
                    {
                        error = this.InternalConnect(connectSocketEvent);
                    }
                }
                else if (this.client.ClientConfiguration.RetryOptions == ClientRetryOptions.Limited)
                {
                    error = this.InternalConnect(connectSocketEvent);
                }
            }

            return error;
        }

        /// <summary>
        /// Connects to the remote host.
        /// </summary>
        /// <param name="connectSocketEvent">The connect event.</param>
        /// <returns>The socket error code.</returns>
        private SocketError InternalConnect(SocketAsyncEventArgs connectSocketEvent)
        {
            if (!this.client.Socket.ConnectAsync(connectSocketEvent))
            {
                this.autoConnectEvent.WaitOne(this.client.ClientConfiguration.ConnectionTimeout);
            }

            return connectSocketEvent.SocketError;
        }

        /// <summary>
        /// Release locks on connector.
        /// </summary>
        public void ReleaseConnectorLock() => this.autoConnectEvent.Set();
    }
}
