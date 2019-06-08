using System;

namespace Imgeneus.Network
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PacketHandlerAttribute : Attribute
    {
        /// <summary>
        /// Gets the packet attribute header.
        /// </summary>
        public object Header { get; private set; }

        /// <summary>
        /// Creates a new <see cref="PacketHandlerAttribute"/> instance.
        /// </summary>
        /// <param name="header">The packet attribute header.</param>
        public PacketHandlerAttribute(object header)
        {
            this.Header = header;
        }
    }
}
