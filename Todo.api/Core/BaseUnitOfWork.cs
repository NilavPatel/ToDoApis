using Microsoft.EntityFrameworkCore;

namespace Todo.api.Core
{
    public class BaseUnitOfWork
    {
        protected DbContext _dbContext;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
