namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class WorldConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30810;
        private readonly string defaultName = "Imgeneus";

        /// <summary>
        /// Gets or sets the world's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Inter-Server configuration
        /// </summary>
        public InterServerConfiguration InterServerConfiguration { get; set; }

        public WorldConfiguration()
        {
            this.Port = this.defaultPort;
            this.Name = this.defaultName;
            this.InterServerConfiguration = new InterServerConfiguration();
        }
    }
}
