﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Todo.api.Core
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(long id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> FromSql(string name, List<SqlParameter> parameters);
    }
}
