namespace Imgeneus.Core.Structures.Configuration
{
    public sealed class WorldConfiguration : BaseConfiguration
    {
        private readonly int defaultPort = 30810;
        private readonly string defaultName = "Imgeneus";

        /// <summary>
        /// Gets or sets the world's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the world's name.
        /// </summary>
        public string Name { get; set; }

        public WorldConfiguration()
        {
            this.Port = defaultPort;
            this.Name = defaultName;
        }
    }
}
