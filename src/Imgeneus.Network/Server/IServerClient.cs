using Imgeneus.Network.Common;

namespace Imgeneus.Network.Server
{
    public interface IServerClient : IConnection
    {
        /// <summary>
        /// Gets the client's parent server.
        /// </summary>
        IServer Server { get; }

    }
}
