namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class LoginConfiguration : BaseConfiguration
    {
        /// <summary>
        /// Gets or sets the client build version
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the password encryption key.
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
