using Imgeneus.Core;

namespace Imgeneus.Login
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            ConsoleAppBootstrapper.CreateApp()
                .SetConsoleTitle("Imgeneus - Login Server")
                .SetCulture("en-US")
                .UseStartup<LoginServerStartup>()
                .Run();
        }
    }
}
