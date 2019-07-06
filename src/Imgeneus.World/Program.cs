using Imgeneus.Core;

namespace Imgeneus.World
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            ConsoleAppBootstrapper.CreateApp()
                .SetConsoleTitle("Imgeneus - World Server")
                .SetCulture("en-US")
                .UseStartup<WorldServerStartup>()
                .Run();
        }
    }
}
