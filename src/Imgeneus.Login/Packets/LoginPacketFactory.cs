using Imgeneus.Network.Data;
using Imgeneus.Network.InternalServer;
using Imgeneus.Network.Packets;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Imgeneus.Login.Packets
{
    public static class LoginPacketFactory
    {
        public static void SendLoginHandshake(LoginClient client)
        {
            using Packet packet = new Packet(PacketType.LOGIN_HANDSHAKE);

            var rsa = new RSACryptoServiceProvider(1024).ExportParameters(false);

            packet.Write<byte>(0);
            packet.Write<byte>((byte)rsa.Exponent.Length);
            packet.Write<byte>((byte)rsa.Modulus.Length);
            packet.Write<byte[]>(rsa.Exponent);
            packet.Write<byte[]>(new byte[61]);
            packet.Write<byte[]>(rsa.Modulus);

            client.SendPacket(packet);

        }
    }
}
