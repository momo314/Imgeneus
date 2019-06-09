using Imgeneus.Network.Common;
using Imgeneus.Network.Data;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Imgeneus.Network.Server
{
    public abstract class ServerClient : Connection, IServerClient
    {
        /// <inheritdoc />
        public IServer Server { get; internal set; }

        /// <inheritdoc />
        public string RemoteEndPoint { get; }

        /// <summary>
        /// Creates a new <see cref="ServerClient"/> instance.
        /// </summary>
        /// <param name="acceptedSocket">Net User socket</param>
        protected ServerClient(IServer server, Socket acceptedSocket)
            : base(acceptedSocket)
        {
            this.Server = server;
            this.RemoteEndPoint = acceptedSocket.RemoteEndPoint.ToString();
        }

        /// <inheritdoc />
        public abstract void HandlePacket(IPacketStream packet);

        /// <inheritdoc />
        public virtual void SendPacket(IPacketStream packet)
            => this.Server.SendPacketTo(this, packet.Buffer);

        /// <inheritdoc />
        public virtual void SendPacketTo(IServerClient client, IPacketStream packet)
            => this.Server.SendPacketTo(client, packet.Buffer);

        /// <inheritdoc />
        public virtual void SendPacketTo(IEnumerable<IServerClient> clients, IPacketStream packet)
            => this.Server.SendPacketTo(clients, packet.Buffer);

        /// <inheritdoc />
        public virtual void SendPacketToAll(IPacketStream packet)
            => this.Server.SendPacketToAll(packet.Buffer);
    }
}
