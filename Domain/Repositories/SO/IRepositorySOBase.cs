using System.Linq.Expressions;

namespace Domain.Repositories.SO
{
    public interface IRepositorySOBase<T>
    {
        IQueryable<T> GetAll(bool trackChanges);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expressions, bool trackChanges);

        void Create(T entity);

        void Delete(T entity);
    }
}
