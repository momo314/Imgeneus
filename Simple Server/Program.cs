using Imgeneus.Network.Client;
using Imgeneus.Network.Server;
using System.Threading;

namespace Simple_Server
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(ClientThread);
            thread.Start();

            Server server = new Server();
            server.Start();
        }

        public static void ClientThread()
        {
            Thread.Sleep(1000);
            Client client = new Client();
            client.Connect();
        }
    }
}
