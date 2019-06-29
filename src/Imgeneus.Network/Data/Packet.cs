using Imgeneus.Network.Packets;
using System.IO;

namespace Imgeneus.Network.Data
{
    public sealed class Packet : PacketStream
    {
        /// <inheritdoc />
        public override byte[] Buffer => this.BuildBuffer();

        /// <summary>
        /// Creates a new <see cref="Packet"/> in write-only mode.
        /// </summary>
        public Packet(PacketType packetType)
        {
            // The packet size
            this.Write<ushort>(0);

            // The packet type
            this.Write<ushort>((ushort)packetType);
            this.PacketType = packetType;
        }

        /// <summary>
        /// Creates a new <see cref="Packet"/> in read-only mode.
        /// </summary>
        /// <param name="buffer"></param>
        public Packet(byte[] buffer)
            : base(buffer)
        {

        }

        private byte[] BuildBuffer()
        {
            long oldPosition = this.Position;
            this.Seek(0, SeekOrigin.Begin);
            this.Write((ushort)this.Length);
            this.Seek(oldPosition, SeekOrigin.Begin);

            byte[] buffer = new byte[this.Length];

            System.Buffer.BlockCopy(base.Buffer, 0, buffer, 0, (int)this.Length);

            return buffer;
        }
    }
}
