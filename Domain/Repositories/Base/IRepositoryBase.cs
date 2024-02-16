using System.Linq.Expressions;

namespace Domain.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll(bool trackChanges);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expressions, bool trackChanges);

        void Create(T entity);

        void Delete(T entity);
    }
}