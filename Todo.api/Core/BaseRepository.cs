using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Todo.api.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IReadOnlyList<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IReadOnlyList<T> List(ISpecification<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public T FirstOrDefault(ISpecification<T> spec)
        {
            return ApplySpecification(spec).FirstOrDefault();
        }

        public int Count(ISpecification<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        // only work for SQL, in memory is no-sql database
        public List<T> FromSql(string name, params object[] parameters)
        {
            return _dbContext.Set<T>().FromSql(name, parameters).ToList();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
