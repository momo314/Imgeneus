namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class LoginConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30800;
        private readonly string defaultEncryptionKey = "Shaiya";

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
            InterServerConfiguration = new InterServerConfiguration();
        }
    }
}
