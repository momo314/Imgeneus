namespace Imgeneus.Network.InternalServer
{
    public sealed class WorldServerInfo
    {
        /// <summary>
        /// Gets or sets the world server unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the world server host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the world server name.
        /// </summary>
        public string Name { get; set; }

        public WorldServerInfo(int id, string host, string name)
        {
            this.Id = id;
            this.Host = host;
            this.Name = name;
        }
    }
}
