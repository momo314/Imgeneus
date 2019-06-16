namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class WorldConfiguration : BaseConfiguration
    {
        /// <summary>
        /// Gets or sets the world's id.
        /// </summary>
        public int Id { get; set; }

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
            this.Port = 30810;
            this.Name = "Ingeneus";
            InterServerConfiguration = new InterServerConfiguration();
        }
    }
}
