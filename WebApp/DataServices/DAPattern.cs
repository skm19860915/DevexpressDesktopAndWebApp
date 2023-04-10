using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using WebApp.DataServices;
using BlitzerCore.DataAccess;

namespace WebApp.DataServices
{
   
    public class DAPattern<TEntity> : IDataAccess<TEntity> where TEntity : class
    {
        public int Save(TEntity aModel, IDbContext aContext)
        {
            return Save(aModel, null, aContext);
        }

        public int Save(TEntity aModel, object aKey, IDbContext aContext)
        {
            if ((aKey == null ) || (aKey is int && (int)aKey == 0) || (aKey is string && (string)aKey == ""))
                ((RepositoryContext)aContext).Set<TEntity>().Add(aModel);
           else
                ((RepositoryContext)aContext).Set<TEntity>().Update(aModel);

            return aContext.SaveChanges();
        }

        public void Delete(TEntity model, IDbContext aContext)
        {
            ((RepositoryContext)aContext).Set<TEntity>().Remove(model);
        }

        public void DeleteById(object Id, IDbContext aContext)
        {
            TEntity entity = ((RepositoryContext)aContext).Set<TEntity>().Find(Id);
            //aContext.Set<TEntity>() (entity);
        }

        public IEnumerable<TEntity> GetAll(IDbContext aContext)
        {
            return ((RepositoryContext)aContext).Set<TEntity>().ToList();
        }

        public TEntity Get(object Id, IDbContext aContext)
        {
            return ((RepositoryContext)aContext).Set<TEntity>().Find(Id);
        }
    }
}
