namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class LoginConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30800;
        private readonly string defaultEncryptionKey = "Shaiya";

        /// <summary>
        /// Gets or sets the password encryption key.
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Gets or sets the Inter-Server configuration
        /// </summary>
        public CoreConfiguration InterServerConfiguration { get; set; }

        public LoginConfiguration()
        {
            this.Port = this.defaultPort;
            this.EncryptionKey = this.defaultEncryptionKey;
            this.InterServerConfiguration = new CoreConfiguration();
        }
    }
}
