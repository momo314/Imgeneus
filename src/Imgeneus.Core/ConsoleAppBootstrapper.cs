using System;
using System.Globalization;

namespace Imgeneus.Core
{
    public sealed class ConsoleAppBootstrapper : IDisposable
    {
        private IProgramStartup startup;

        private ConsoleAppBootstrapper()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConsoleAppBootstrapper"/> instance.
        /// </summary>
        /// <returns>The new instance created.</returns>
        public static ConsoleAppBootstrapper CreateApp() => new ConsoleAppBootstrapper();
        /// <summary>
        /// Sets the console title.
        /// </summary>
        /// <param name="title">The Console title.</param>
        /// <returns>The new instance created.</returns>
        public ConsoleAppBootstrapper SetConsoleTitle(string title)
        {
            Console.Title = title;
            return this;
        }

        /// <summary>
        /// Sets the current program culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The new instance created.</returns>
        public ConsoleAppBootstrapper SetCulture(string culture)
        {
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);

            return this;
        }

        /// <summary>
        /// Defines the program startup class.
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <returns>The new instance created.</returns>
        public ConsoleAppBootstrapper UseStartup<TStartup>() where TStartup : class, IProgramStartup
        {
            this.startup = Activator.CreateInstance<TStartup>() as IProgramStartup;
            this.startup.Configure();

            AppDomain.CurrentDomain.ProcessExit += (sender, args) => this.startup.Dispose();

            return this;
        }

        /// <summary>
        /// Start the program.
        /// </summary>
        public void Run()
        {
            if (this.startup == null)
            {
                throw new InvalidProgramException("No startup class specified.");
            }

            this.startup.Run();
        }

        /// <summary>
        /// Releases the program resources.
        /// </summary>
        public void Dispose() => this.startup?.Dispose();
    }
}
