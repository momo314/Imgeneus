using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Imgeneus.Network.Server.Internal
{
    internal sealed class ServerReceiver
    {
        private readonly IServer server;

        /// <summary>
        /// Gets the receive <see cref="SocketAsyncEventArgs"/> pool
        /// </summary>
        public ConcurrentStack<SocketAsyncEventArgs> ReadPool { get; }

        /// <summary>
        /// Creates a new <see cref="ServerReceiver"/> instance.
        /// </summary>
        /// <param name="server">The parent server.</param>
        public ServerReceiver(IServer server)
        {
            this.server = server;
            this.ReadPool = new ConcurrentStack<SocketAsyncEventArgs>();
        }
    }
}
