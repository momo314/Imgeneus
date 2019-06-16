namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class LoginConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30800;
        private readonly string defaultEncryptionKey = "Shaiya";
        private readonly int defaultMaxNumberOfConnections = 1000;

        /// <summary>
        /// Gets the maximum number of connections allowed on the server.
        /// </summary>
        public int MaximumNumberOfConnections { get; set; }

        /// <summary>
        /// Gets or sets the client build version
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the password encryption key.
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Gets or sets the Inter-Server configuration
        /// </summary>
        public InterServerConfiguration InterServerConfiguration { get; set; }

        public LoginConfiguration()
        {
            this.Port = defaultPort;
            this.EncryptionKey = defaultEncryptionKey;
            this.MaximumNumberOfConnections = defaultMaxNumberOfConnections;
            InterServerConfiguration = new InterServerConfiguration();
        }
    }
}
