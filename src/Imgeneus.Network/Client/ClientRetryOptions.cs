namespace Imgeneus.Network.Client
{
    public enum ClientRetryOptions
    {
        /// <summary>
        /// The client only try to connect one time.
        /// </summary>
        Onetime = 0,

        /// <summary>
        /// The client will try to connect a specific amount of times.
        /// </summary>
        Limited = 1,

        /// <summary>
        /// The client will try infinitely to connect to the server.
        /// </summary>
        Infinite = 2,
    }
}
