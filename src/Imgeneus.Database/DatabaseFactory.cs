using Imgeneus.Core.Helpers;
using Imgeneus.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Imgeneus.Database
{
    public class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.ConfigureCorrectDatabase();

            return new DatabaseContext(optionsBuilder.Options);
        }

        public IDatabase GetDatabase()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.ConfigureCorrectDatabase();
            DatabaseContext databaseContext = new DatabaseContext(optionsBuilder.Options);
            return new Database(databaseContext);
        }
    }
}
