namespace Imgeneus.Core.Structures.Configuration
{
    public class InterServerConfiguration : BaseConfiguration
    {
        /// <summary>
        /// Get or sets the Inter-Server password.
        /// </summary>
        public string Password { get; set; }

        public InterServerConfiguration()
        {
            this.Port = 500;
            this.Password = "your_password";
        }
    }
}
