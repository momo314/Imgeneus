namespace Imgeneus.Core.Structures.Configuration
{
    public class BaseConfiguration
    {
        private readonly string defaultHost = "127.0.0.1";
        private readonly int defaultMaxNumberOfConnections = 1000;

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets the maximum number of connections allowed on the server.
        /// </summary>
        public int MaximumNumberOfConnections { get; set; }

        public BaseConfiguration()
        {
            this.Host = defaultHost;
            this.MaximumNumberOfConnections = defaultMaxNumberOfConnections;
        }

    }
}
