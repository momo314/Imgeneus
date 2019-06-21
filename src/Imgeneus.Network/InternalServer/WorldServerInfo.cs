using Imgeneus.Network.Packets.Login;

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

        /// <summary>
        /// Gets the maximum number of connected users.
        /// </summary>
        public ushort MaxAllowedUsers { get; set; }

        /// <summary>
        /// Gets the number of connected users.
        /// </summary>
        public ushort ConnectedUsers { get; set; }

        /// <summary>
        /// The current WorldStatus.
        /// </summary>
        public WorldState WorldStatus { get; set; }

        public WorldServerInfo(int id, string host, string name, ushort connectedusers, ushort maxAllowedUsers, WorldState status)
        {
            this.Id = id;
            this.Host = host;
            this.Name = name;
            this.ConnectedUsers = connectedusers;
            this.MaxAllowedUsers = maxAllowedUsers;
            this.WorldStatus = status;
        }
    }
}
