using System;
using System.Net.Sockets;

namespace Imgeneus.Network.Common
{
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Gets the connection unique idenfified.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the connection socket.
        /// </summary>
        Socket Socket { get; }
    }
}
