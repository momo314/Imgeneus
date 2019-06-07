using Imgeneus.Network.Common;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Imgeneus.Network.Server.Internal
{
    internal sealed class ServerSender : Sender
    {
        private readonly AutoResetEvent autoSendEvent;

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
            this.autoSendEvent = new AutoResetEvent(false);
        }

        /// <inheritdoc />
        protected override void SendPacket(PacketData packetData)
        {
            if (this.WritePool.TryPop(out SocketAsyncEventArgs writeSocket))
            {
                writeSocket.SetBuffer(packetData.Data, 0, packetData.Data.Length);
                writeSocket.UserToken = packetData.Connection;

                if (!packetData.Connection.Socket.SendAsync(writeSocket))
                {
                    this.SendOperationCompleted(writeSocket);
                }
            }
            else
            {
                this.autoSendEvent.WaitOne();
                this.SendPacket(packetData);
            }
        }

        /// <inheritdoc />
        public override void SendOperationCompleted(SocketAsyncEventArgs e)
        {
            this.WritePool.Push(e);
            this.autoSendEvent.Set();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.WritePool.Clear();
        }
    }
}
