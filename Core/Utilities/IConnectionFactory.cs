using BlitzerCore.Models;

namespace BlitzerCore.Models
{
    public interface IConnectionFactory
    {
        IDbContext GetDbContext();
    }
}
