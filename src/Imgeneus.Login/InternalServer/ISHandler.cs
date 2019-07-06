using Imgeneus.Network;
using Imgeneus.Network.Data;
using Imgeneus.Network.InternalServer;
using Imgeneus.Network.Packets;

namespace Imgeneus.Login.InternalServer
{
    public static class ISHandler
    {
        [PacketHandler(PacketType.AUTH_SERVER)]
        public static void OnAuthenticateServer(ISClient client, IPacketStream packet)
        {
            byte id = 1;
            byte[] host = packet.Read<byte>(4);
            string name = packet.ReadString(32);
            int buildVersion = packet.Read<int>();
            ushort maxConnections = packet.Read<ushort>();

            WorldServerInfo world = new WorldServerInfo(id, host, name, buildVersion, maxConnections);
            client.SetWordServerInfo(world);
        }
    }
}
