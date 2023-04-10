using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public interface IDataAccess<TEntity> where TEntity : class
    {
        int Save(TEntity model, object aKey, IDbContext aContext);

        IEnumerable<TEntity> GetAll(IDbContext aContext);

        TEntity Get(object Id, IDbContext aContext);

        void Delete(TEntity model, IDbContext aContext);

        void DeleteById(object Id, IDbContext aContext);
    }
}
