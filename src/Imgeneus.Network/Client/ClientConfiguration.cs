using System;
using System.Collections.Generic;
using System.Text;

namespace Imgeneus.Network.Client
{
    public class ClientConfiguration
    {
        private const int defaultBufferSize = 2048;
        private const int defaultConnectionTimeout = 5000;

        /// <summary>
        /// Gets the <see cref="Client"/> host to connect.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the <see cref="Client"/> port to connect.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Gets the <see cref="Client"/> data buffer size to allocate during construction.
        /// </summary>
        public int BufferSize { get; }

    }
}
