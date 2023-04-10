using BlitzerCore.Utilities;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Desktop.DataServices;

namespace Desktop.DataServices
{
    public class ConcreteFactory : IConnectionFactory
    {
        public IDbContext GetDbContext() {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            //optionsBuilder.UseSqlServer (Logger.ConnectionString);

            var lContext = new Desktop.DataServices.RepositoryContext(optionsBuilder.Options);
            return lContext;
        }

    }
}
