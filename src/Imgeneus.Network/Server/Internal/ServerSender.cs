using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Imgeneus.Network.Server.Internal
{
    internal sealed class ServerSender
    {
        private readonly AutoResetEvent autoResetEvent;

        /// <summary>
        /// Gets the sender <see cref="SocketAsyncEventArgs"/> pool.
        /// </summary>
        public ConcurrentStack<SocketAsyncEventArgs> WritePool { get; }

        /// <summary>
        /// Creates a new <see cref="ServerSender"/> instace.
        /// </summary>
        public ServerSender()
        {
            this.WritePool = new ConcurrentStack<SocketAsyncEventArgs>();
            this.autoResetEvent = new AutoResetEvent(false);
        }
    }
}
