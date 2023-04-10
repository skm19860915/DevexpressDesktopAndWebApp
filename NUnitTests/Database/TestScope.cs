using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using WebApp.DataServices;

namespace NUnitTests.Database
{
    public class TestScope : IDisposable
    {
        private readonly SqlDatabase database = null;

        public static async Task<TestScope> CreateNewAsync(int attempts = 0)
        {
            const int maxRetries = 5;

            var scope = new TestScope();

            try
            {
                await scope.CreateDatabaseAsync();
                return scope;
            }
            catch (Exception exception)
            {
                scope.Dispose();

                if (attempts < maxRetries)
                {
                    Console.WriteLine($"An error occurred while creating the test scope, will try {maxRetries - attempts - 1} more times to create test scope. Error was:\n{exception}");
                    return await CreateNewAsync(attempts + 1);
                }

                Console.WriteLine($"Tried {maxRetries} times to create test context but failed, see logs for more details");
                throw;
            }
        }

        private TestScope()
        {
            //todo: Uncomment
            //database = new SqlDatabase(TestContext.CurrentContext);
        }

        private async Task CreateDatabaseAsync()
        {
            Console.WriteLine($"Attempting to create database {database.Name}.");
            using (var context = CreateNewContext())
            {
                await context.Database.MigrateAsync();
            }
            Console.WriteLine($"Successfully to created database.");
        }


        private RepositoryContext CreateNewContext()
        {
            var builder = new DbContextOptionsBuilder<RepositoryContext>();
            builder.UseSqlServer(database.ConnectionString);

            return new RepositoryContext(builder.Options);
        }

        public async Task SeedAsync(Action<RepositoryContext> seedAction)
        {
            using (var context = CreateNewContext())
            {
                seedAction(context);
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            database?.Dispose();
        }
    }
}

