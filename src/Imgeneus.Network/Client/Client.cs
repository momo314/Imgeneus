using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Imgeneus.Network.Client
{
    /// <summary>
    /// Provides a mechanism for creating managed TCP clients.
    /// </summary>
    public class Client
    {
        private readonly Socket socket;

        /// <summary>
        /// Gets host IP address.
        /// </summary>
        public string IPAddress { get; }

        /// <summary>
        /// Gets the host port.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Creates a new <see cref="Client"/> instance.
        /// </summary>
        public Client()
        {
            this.IPAddress = "127.0.0.1";
            this.Port = 30800;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connects to a remote host.
        /// </summary>
        public void Connect()
        {
            // Inform the user that the client is being initialised
            Console.WriteLine("initialising client...");

            this.socket.Connect(IPAddress, Port);

            if (socket.Connected)
            {
                // Inform the user that the client is connected to a servers
                Console.WriteLine($"Client Connected to {socket.RemoteEndPoint}");

                byte[] buffer = new byte[50];

                // Receive a packet
                socket.Receive(buffer, 50, SocketFlags.None);

                // Print the receive packet
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Encoding.UTF8.GetString(buffer));

                // Send a packet
                socket.Send(Encoding.UTF8.GetBytes("Hello from client"));

            }

        }

        /// <summary>
        /// Disconnect the current socket.
        /// </summary>
        public void Disconnect()
        {
            this.socket.Disconnect(false);
            this.socket.Close();
            this.socket.Dispose();
        }
    }
}
