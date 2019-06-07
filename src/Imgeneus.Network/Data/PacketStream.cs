using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Imgeneus.Network.Data
{
    public class PacketStream : MemoryStream, IPacketStream
    {
        private readonly BinaryReader? reader;
        private readonly BinaryWriter? writer;

        /// <inheritdoc />
        public PacketStateType State { get; }

        public bool IsEndOfStream => this.Position >= this.Length;

        /// <inheritdoc />
        public virtual byte[] Buffer => this.GetStreamBuffer();

        /// <summary>
        /// Creates and initializes a new <see cref="PacketStream"/> instance in write-only mode.
        /// </summary>
        public PacketStream()
        {
            this.writer = new BinaryWriter(this);
            this.State = PacketStateType.Write;
        }

        /// <summary>
        /// Creates and initializes a new <see cref="PacketStream"/> instance in read-only mode.
        /// </summary>
        /// <param name="buffer">Input buffer</param>
        public PacketStream(byte[] buffer)
            : base(buffer, 0, buffer.Length, false, true)
        {
            this.reader = new BinaryReader(this);
            this.State = PacketStateType.Read;
        }

        /// <inheritdoc />
        public virtual T Read<T>()
        {
            if (this.State != PacketStateType.Read)
            {
                throw new InvalidOperationException("Packet is in write-only mode.");
            }

            return ReadMethods.TryGetValue(typeof(T), out Func<BinaryReader, object> method)
                ? (T)method.Invoke(this.reader)
                : default(T);
        }

        /// <inheritdoc />
        public virtual void Write<T>(T value)
        {
            if (this.State != PacketStateType.Write)
            {
                throw new InvalidOperationException("Packet is in read-only mode.");
            }

            if (WriteMethods.TryGetValue(typeof(T), out Action<BinaryWriter, object> method))
                method.Invoke(this.writer, value);
        }

        /// <inheritdoc />
        public void Skip(int byteCount) => this.Position += byteCount;

        /// <summary>
        /// Gets the stream buffer.
        /// </summary>
        /// <returns></returns>
        private byte[] GetStreamBuffer() => this.TryGetBuffer(out ArraySegment<byte> buffer) ? buffer.ToArray() : new byte[0];

        /// <summary>
        /// Read methods dictionary.
        /// </summary>
        private static readonly Dictionary<Type, Func<BinaryReader, object>> ReadMethods = new Dictionary<Type, Func<BinaryReader, object>>
        {
            { typeof(char), reader => reader.ReadChar() },
            { typeof(byte), reader => reader.ReadByte() },
            { typeof(sbyte), reader => reader.ReadSByte() },
            { typeof(bool), reader => reader.ReadBoolean() },
            { typeof(ushort), reader => reader.ReadUInt16() },
            { typeof(short), reader => reader.ReadInt16() },
            { typeof(uint), reader => reader.ReadUInt32() },
            { typeof(int), reader => reader.ReadInt32() },
            { typeof(ulong), reader => reader.ReadUInt64() },
            { typeof(long), reader => reader.ReadInt64() },
            { typeof(float), reader => reader.ReadSingle() },
            { typeof(double), reader => reader.ReadDouble() },
            { typeof(decimal), reader => reader.ReadDecimal() },
            { typeof(byte[]), reader => reader.ReadBytes(count: reader.ReadInt32()) },
            { typeof(string), reader => new string(reader.ReadChars(count: reader.ReadInt32())) },
        };

        /// <summary>
        /// Write methods dictionary.
        /// </summary>
        private static readonly Dictionary<Type, Action<BinaryWriter, object>> WriteMethods = new Dictionary<Type, Action<BinaryWriter, object>>
        {
            { typeof(char), (writer, value) => writer.Write((char)value) },
            { typeof(byte), (writer, value) => writer.Write((byte)value) },
            { typeof(sbyte), (writer, value) => writer.Write((sbyte)value) },
            { typeof(bool), (writer, value) => writer.Write((bool)value) },
            { typeof(ushort), (writer, value) => writer.Write((ushort)value) },
            { typeof(short), (writer, value) => writer.Write((short)value) },
            { typeof(uint), (writer, value) => writer.Write((uint)value) },
            { typeof(int), (writer, value) => writer.Write((int)value) },
            { typeof(ulong), (writer, value) => writer.Write((ulong)value) },
            { typeof(long), (writer, value) => writer.Write((long)value) },
            { typeof(float), (writer, value) => writer.Write((float)value) },
            { typeof(double), (writer, value) => writer.Write((double)value) },
            { typeof(decimal), (writer, value) => writer.Write((decimal)value) },
            { typeof(byte[]),
                (writer, value) =>
                {
                    writer.Write(((byte[])value).Length);
                    if (((byte[])value).Length > 0)
                        writer.Write((byte[])value);
                }
            },
            { typeof(string),
                (writer, value) =>
                {
                    writer.Write(value.ToString().Length);
                    if (value.ToString().Length > 0)
                        writer.Write(Encoding.UTF8.GetBytes(value.ToString()));
                }
            }
        };
    }
}
