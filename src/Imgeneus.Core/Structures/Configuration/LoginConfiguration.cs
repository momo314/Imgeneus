namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class LoginConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30800;

        /// <summary>
        /// Gets or sets the client build version
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the password encryption key.
        /// </summary>
        public string EncryptionKey { get; set; }

        public LoginConfiguration()
        {
            this.Port = 30800;
            this.EncryptionKey = "Shaiya";
        }
    }
}
