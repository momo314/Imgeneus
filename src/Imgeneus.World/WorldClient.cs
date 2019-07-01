using Imgeneus.Core.DependencyInjection;
using Imgeneus.Network;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;
using Imgeneus.Network.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Sockets;

namespace Imgeneus.World
{
    public sealed class WorldClient : ServerClient
    {
        private readonly ILogger<WorldClient> logger;

        public WorldClient(IServer server, Socket acceptedSocket)
            : base(server, acceptedSocket)
        {
            this.logger = DependencyContainer.Instance.Resolve<ILogger<WorldClient>>();
        }

        public override void HandlePacket(IPacketStream packet)
        {
            if (this.Socket == null)
            {
                this.logger.LogTrace("Skip to handle packet. Reason: client is no more connected.");
                return;
            }

            try
            {
                if (!PacketHandler<WorldClient>.Invoke(this, packet, packet.PacketType))
                {
                    if (Enum.IsDefined(typeof(PacketType), packet.PacketType))
                    {
                        this.logger.LogWarning("Received an unimplemented packet {0} from {2}.", packet.PacketType, this.RemoteEndPoint);
                    }
                    else
                    {
                        this.logger.LogWarning("Received an unknown packet 0x{0} from {1}.", ((ushort)packet.PacketType).ToString("X2"), this.RemoteEndPoint);
                    }
                }
            }
            catch (Exception exception)
            {
                this.logger.LogError("Packet handle error from {0}. {1}", this.RemoteEndPoint, exception.Message);
                this.logger.LogDebug(exception.InnerException?.StackTrace);
            }
        }
    }
}
