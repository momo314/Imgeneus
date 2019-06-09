namespace Imgeneus.Core.Structures.Configuration
{
    public class BaseConfiguration
    {
        private readonly string defaultHost = "127.0.0.1";

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        public BaseConfiguration()
        {
            this.Host = defaultHost;
        }

    }
}
