namespace Imgeneus.Network.Packets
{
    public enum PacketType : ushort
    {
        // Login opcodes
        LOGIN_TERMINATE = 0x0107,
        LOGIN_HANDSHAKE = 0xA101,
        LOGIN_REQUEST = 0xA102,
        SERVER_LIST = 0xA201,
        SELECT_SERVER = 0xA202,
    }
}
