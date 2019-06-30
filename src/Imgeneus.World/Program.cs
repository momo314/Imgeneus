using Imgeneus.Core;

namespace Imgeneus.World
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleAppBootstrapper.CreateApp()
                .SetConsoleTitle("Rhisis - World Server")
                .SetCulture("en-US")
                .UseStartup<WorldServerStartup>()
                .Run();
        }
    }
}
