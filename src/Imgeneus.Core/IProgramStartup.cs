using System;

namespace Imgeneus.Core
{
    public interface IProgramStartup : IDisposable
    {
        /// <summary>
        /// Configure the program.
        /// </summary>
        void Configure();

        /// <summary>
        /// Runs the program logic.
        /// </summary>
        void Run();
    }
}
