using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;

namespace Imgeneus.World.Packets
{
    public static partial class WorldPacketFactory
    {
        public static void SendAccountFaction(WorldClient client, byte faction, byte maxMode)
        {
            using var packet = new Packet(PacketType.ACCOUNT_FACTION);
            packet.Write<byte>(faction);
            packet.Write<byte>(maxMode);

            client.SendPacket(packet);
        }
    }
}
