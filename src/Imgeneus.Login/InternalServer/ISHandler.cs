using Imgeneus.Network;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;
using System;

namespace Imgeneus.Login.InternalServer
{
    public static class ISHandler
    {
        [PacketHandler(PacketType.AUTH_SERVER)]
        public static void OnAuthenticateServer(LoginClient client, IPacketStream packet)
        {
            throw new NotImplementedException();
            // TODO: Disconnect client from Internal-Server and World Server
        }
    }
}
