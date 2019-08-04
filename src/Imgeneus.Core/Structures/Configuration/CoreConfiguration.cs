namespace Imgeneus.Core.Structures.Configuration
{
    public class CoreConfiguration : BaseConfiguration
    {
        /// <summary>
        /// Get or sets the Inter-Server password.
        /// </summary>
        public string Password { get; set; }

        public CoreConfiguration()
        {
            this.Port = 500;
            this.Password = "your_password";
        }
    }
}
