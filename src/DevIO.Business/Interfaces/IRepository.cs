using AppMvcEasyMode.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task<TEntity> FindById(Guid id);

        Task<List<TEntity>> ListAll();

        Task Update(TEntity entity);

        Task Remove(Guid id);

        //search entity using a wished parameter
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
