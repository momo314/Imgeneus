using System;
using System.Net;

namespace Imgeneus.Network.Common
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Gets an <see cref="IPAddress"/> from an IP string.
        /// </summary>
        /// <param name="ipOrHost">Ip string address.</param>
        /// <returns><Parsed <see cref="IPAddress"/>./returns>
        public static IPAddress BuildIPAddress(string ipString)
        {
            return IPAddress.TryParse(ipString, out IPAddress address) ? address
                : throw new ArgumentException($"Invalid IP address: {ipString}.");
        }

        /// <summary>
        /// Creates a new <see cref="IPEndPoint"/> with an IP string and port number.
        /// </summary>
        /// <param name="ipString">IP string address.</param>
        /// <param name="port">Port number.</param>
        /// <returns>A new <see cref="IPEndPoint"/> instance.</returns>
        public static IPEndPoint CreateIPEndPoint(string ipString, int port)
        {
            IPAddress address = BuildIPAddress(ipString);

            if (port <= 0)
            {
                throw new ArgumentException($"Invalid port: {port}.");
            }

            return new IPEndPoint(address, port);
        }
    }
}
