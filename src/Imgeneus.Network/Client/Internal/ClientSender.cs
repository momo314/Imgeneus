using Imgeneus.Network.Common;
using System.Net.Sockets;

namespace Imgeneus.Network.Client.Internal
{
    /// <summary>
    /// Sends packets to remote host.
    /// </summary>
    internal sealed class ClientSender : Sender
    {
        /// <summary>
        /// Gets the sending <see cref="SocketAsyncEventArgs"/>.
        /// </summary>
        public SocketAsyncEventArgs SendSocketEventArgs { get; }

        /// <summary>
        /// Creates a new <see cref="ClientSender"/> instance.
        /// </summary>
        /// <param name="sendSocketEvent">The socket event args.</param>
        public ClientSender(SocketAsyncEventArgs sendSocketEvent)
        {
            this.SendSocketEventArgs = sendSocketEvent;
        }

        /// <inheritdoc />
        public override void SendPacket(PacketData packetData)
        {
            SendSocketEventArgs.SetBuffer(packetData.Data, 0, packetData.Data.Length);
            SendSocketEventArgs.UserToken = packetData.Connection;

            if (packetData.Connection.Socket.SendAsync(SendSocketEventArgs))
            {
                this.autoSendEvent.WaitOne();
            }
            else
            {
                this.SendOperationCompleted(SendSocketEventArgs);
            }
        }

        /// <inheritdoc />
        public override void SendOperationCompleted(SocketAsyncEventArgs e)
        {
            this.autoSendEvent.Set();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            this.SendSocketEventArgs.Dispose();
            base.Dispose(disposing);
        }
    }
}
