using Imgeneus.Database.Entities;
using Imgeneus.Network.Data;
using Imgeneus.Network.Packets;
using Imgeneus.Network.Packets.Login;
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

        public static void AuthenticationFailed(LoginClient client, AuthenticationResult result)
        {
            using var packet = new Packet(PacketType.LOGIN_REQUEST);

            packet.Write<byte>((byte)result);
            packet.Write<byte[]>(new byte[21]);

            client.SendPacket(packet);
        }

        public static void AuthenticationFailed(LoginClient client, AuthenticationResult result, DbUser user)
        {
            using var packet = new Packet(PacketType.LOGIN_REQUEST);

            packet.Write<byte>((byte)result);
            packet.Write<int>(user.Id);
            packet.Write<byte>(user.Authority);
            packet.Write<byte[]>(client.Id.ToByteArray());

            client.SendPacket(packet);
        }
    }
}
