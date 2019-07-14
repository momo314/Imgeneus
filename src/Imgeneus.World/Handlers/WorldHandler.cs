using Imgeneus.Network;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;

namespace Imgeneus.World.Handlers
{
    internal static class WorldHandler
    {
        [PacketHandler(PacketType.GAME_HANDSHAKE)]
        public static void OnGameHandshake(WorldClient client, IPacketStream paccket)
        {
            using var sendPacket = new Packet(PacketType.GAME_HANDSHAKE);

            sendPacket.Write<byte>(0);
            client.SendPacket(sendPacket);
        }
    }
}
