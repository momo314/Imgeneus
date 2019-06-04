using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Imgeneus.Network.Server
{
    /// <summary>
    /// Provides a basic mechanism to start a TCP server.
    /// </summary>
    public class Server
    {
        private readonly Socket socket;

        /// <summary>
        /// Gets the host IP.
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// Gets the host Port.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Creates a new <see cref="Server"/> instance.
        /// </summary>
        public Server()
        {
            this.IP = "127.0.0.1";
            this.Port = 30800;

            // creates a new TCP socket
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public void Start()
        {
            // Inform the user that the server is being initialised
            Console.WriteLine("Initialising server...");
            
            // Creates a new End point
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), Port);

            // Bind the server options
            this.socket.Bind(ep);

            // Start to listen
            this.socket.Listen(50);

            Console.WriteLine($"Server started and listening on {ep.ToString()}");

            // Start to accept connections
            Socket accept = this.socket.Accept();

            // Inform the user that a new client is connected
            Console.WriteLine($"Client accepted from {accept.RemoteEndPoint.ToString()} ");

            // Send a packet to connected client
            accept.Send(Encoding.UTF8.GetBytes("Hello from server"));

            byte[] buffer = new byte[50];

            // Receive a packet from client
            accept.Receive(buffer);

            // Print the received packet
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            // Close the socket
            this.socket.Close();
            this.socket.Dispose();
            Console.WriteLine($"Server Stopped");
        }
    }
}
