using Imgeneus.Core;

namespace Imgeneus.Login
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleAppBootstrapper.CreateApp()
                .SetConsoleTitle("Imgeneus - Login Server")
                .SetCulture("en-US")
                .UseStartup<LoginServerStartup>()
                .Run();
        }
    }
}
