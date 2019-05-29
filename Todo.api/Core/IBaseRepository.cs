using System.Collections.Generic;

namespace Todo.api.Core
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IReadOnlyList<T> ListAll();
        IReadOnlyList<T> List(ISpecification<T> spec);
        T FirstOrDefaultAsync(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(ISpecification<T> spec);
        List<T> FromSql(string name, params object[] parameters);
    }
}
