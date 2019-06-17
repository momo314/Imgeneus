using Imgeneus.Network;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;
using System;

namespace Imgeneus.Login
{
    public static class LoginHandler
    {
        [PacketHandler(PacketType.LOGIN_HANDSHAKE)]
        public static void OnLoginHandshake(LoginClient client, IPacketStream packet)
        {
            throw new NotImplementedException();
        }

        [PacketHandler(PacketType.LOGIN_REQUEST)]
        public static void OnLoginRequest(LoginClient client, IPacketStream packet)
        {
            throw new NotImplementedException();
        }

        [PacketHandler(PacketType.SELECT_SERVER)]
        public static void OnSelectServer(LoginClient client, IPacketStream packet)
        {
            throw new NotImplementedException();
            // TODO: Check build version and the current world status.
        }

        [PacketHandler(PacketType.LOGIN_TERMINATE)]
        public static void OnCloseConnection(LoginClient client, IPacketStream packet)
        {
            throw new NotImplementedException();
            // TODO: Disconnect client from Internal-Server and World Server
        }
    }
}
