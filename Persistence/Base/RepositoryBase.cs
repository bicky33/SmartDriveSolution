using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System.Linq.Expressions;

namespace Persistence.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SmartDriveContext _dbContext;

        protected RepositoryBase(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public IQueryable<T> GetAll(bool trackChanges) => trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expressions, bool trackChanges) =>
            !trackChanges ? _dbContext.Set<T>().AsNoTracking().Where(expressions) : _dbContext.Set<T>().Where(expressions);
    }
}