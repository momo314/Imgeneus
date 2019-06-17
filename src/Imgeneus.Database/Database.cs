using Imgeneus.Database.Context;
using Imgeneus.Database.Entities;
using Imgeneus.Database.Repositories;
using System.Threading.Tasks;

namespace Imgeneus.Database
{
    public sealed class Database : IDatabase
    {
        /// <inheritdoc />
        public IRepository<DbUser> Users { get; set; }
        
        /// <inheritdoc />
        public IRepository<DbCharacter> Charaters { get; set; }

        /// <inheritdoc />
        public IRepository<DbCharacterItems> CharacterItems { get; set; }

        public DatabaseContext DatabaseContext { get; set; }

        public Database(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
            this.Users = new RepositoryBase<DbUser>(databaseContext);
            this.Charaters = new RepositoryBase<DbCharacter>(databaseContext);
            this.CharacterItems = new RepositoryBase<DbCharacterItems>(databaseContext);
        }

        /// <inheritdoc />
        public void Complete() => this.DatabaseContext.SaveChanges();

        /// <inheritdoc />
        public async Task CompleteAsync() => await this.DatabaseContext.SaveChangesAsync();

        /// <inheritdoc />
        public void Dispose() => this.DatabaseContext.Dispose();

    }
}
