using Imgeneus.Core.Structures.Configuration;
using Imgeneus.Network.Client;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;
using System.Net;

namespace Imgeneus.World.InternalServer
{
    public static class ISPacketFactory
    {
        public static void Authenticate(IClient client, WorldConfiguration configuration)
        {
            using var packet = new Packet(PacketType.AUTH_SERVER);

            packet.Write<byte[]>(IPAddress.Parse(configuration.Host).GetAddressBytes());
            packet.WriteString(configuration.Name, 32);
            packet.Write<int>(configuration.BuildVersion);
            packet.Write<ushort>((ushort)configuration.MaximumNumberOfConnections);

            client.SendPacket(packet);
        }
    }
}
