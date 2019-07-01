using Imgeneus.Network.Data;
using Imgeneus.Network.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Imgeneus.Network
{
    public static class PacketHandler<T>
    {
        private static readonly IDictionary<object, Action<T, IPacketStream>> handlers = new Dictionary<object, Action<T, IPacketStream>>();

        private struct PacketMethodHandler : IEquatable<PacketMethodHandler>
        {
            public readonly PacketHandlerAttribute Attribute;
            public readonly MethodInfo Method;

            public PacketMethodHandler(MethodInfo method, PacketHandlerAttribute attribute)
            {
                this.Method = method;
                this.Attribute = attribute;
            }

            public bool Equals(PacketMethodHandler other)
            {
                return this.Attribute.Header == other.Attribute.Header
                    && this.Attribute.TypeId == other.Attribute.TypeId
                    && this.Method == other.Method;
            }
        }

        /// <summary>
        /// Initialize the packet handler
        /// </summary>
        public static void Initialize()
        {
            // Gets all public statis methods with PacketHandlerAttribute
            IEnumerable<PacketMethodHandler[]> readHandlers = from type in typeof(T).Assembly.GetTypes()
                                                              let typeInfo = type.GetTypeInfo()
                                                              let methodsInfo = typeInfo.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                              let handler = (from x in methodsInfo
                                                                             let attribute = x.GetCustomAttribute<PacketHandlerAttribute>()
                                                                             where attribute != null
                                                                             select new PacketMethodHandler(x, attribute)).ToArray()
                                                              select handler;

            // Save all packet handler in our internal dictionary
            foreach (PacketMethodHandler[] readHandler in readHandlers)
            {
                foreach (PacketMethodHandler methodHandler in readHandler)
                {
                    ParameterInfo[] parameters = methodHandler.Method.GetParameters();

                    if (parameters.Count() != 2 || parameters.First().ParameterType != typeof(T))
                    {
                        continue;
                    }

                    var action = methodHandler.Method.CreateDelegate(typeof(Action<T, IPacketStream>)) as Action<T, IPacketStream>;

                    handlers.Add(methodHandler.Attribute.Header, action);
                }
            }
        }

        /// <summary>
        /// Invoke the packet handler and process it.
        /// </summary>
        /// <param name="invoker">The <see cref="IServerClient"/> packet owner.</param>
        /// <param name="packet">The packet to process.</param>
        /// <param name="opcode">The packet operation code.</param>
        /// <returns>True if the packet is processed correctly.</returns>
        public static bool Invoke(T invoker, IPacketStream packet, object opcode)
        {
            if (handlers.TryGetValue(opcode, out Action<T, IPacketStream> packetHandler))
            {
                try
                {
                    packetHandler.Invoke(invoker, packet);
                }
                catch (Exception ex)
                {
                    throw new MissingMethodException($"An error occurred during the execution of packet handler: {packetHandler}", ex);
                }

                return true;
            }

            return false;
        }
    }
}
