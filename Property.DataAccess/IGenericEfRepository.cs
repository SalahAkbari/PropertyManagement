using System.Collections.Generic;
using System.Threading.Tasks;

namespace Property.DataAccess
{
    public interface IGenericEfRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(int id, bool includeRelatedEntities = false);
        bool Save();
        void Add(TEntity item);
        bool Exists(int id);
        void Delete(TEntity item);
        void Edit(TEntity item, int id);
    }
}
