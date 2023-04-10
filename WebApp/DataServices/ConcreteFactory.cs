using BlitzerCore.Utilities;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DataServices
{
    public class ConcreteFactory : IConnectionFactory
    {
        public IDbContext GetDbContext() {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer (Logger.ConnectionString);

            var lContext = new WebApp.DataServices.RepositoryContext(optionsBuilder.Options);
            return lContext;
        }

    }
}
