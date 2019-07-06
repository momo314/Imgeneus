namespace Imgeneus.Network.Packets
{
    public enum PacketType : ushort
    {
        // Internal Server
        AUTH_SERVER = 5,
        SERVER_INFO = 6,
        UPDATE_SERVER = 7,
        DISSCONNECT_USER = 8,

        // Common
        CLOSE_CONNECTION = 0x0107,

        // Login Server
        LOGIN_HANDSHAKE = 0xA101,
        LOGIN_REQUEST = 0xA102,
        SERVER_LIST = 0xA201,
        SELECT_SERVER = 0xA202,
    }
}
