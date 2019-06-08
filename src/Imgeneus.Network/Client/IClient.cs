using Imgeneus.Network.Data;

namespace Imgeneus.Network.Client
{
    /// <summary>
    /// Provides a mechanism for creating managed TCP clients.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Gets the <see cref="IClient"/> connected state.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets the <see cref="IClient"/> running state.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Connects to a remote server.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects from the remote server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Handle an incoming packet.
        /// </summary>
        /// <param name="packet">The incoming packet.</param>
        void HandlePacket(IPacketStream packet);

        /// <summary>
        /// Sends a packet to the remote server.
        /// </summary>
        /// <param name="packet">The packet stream.</param>
        void SendMessage(IPacketStream packet);
    }
}
