using Todo.api.Core;

namespace Todo.api.Models
{
    public class ToDoUnitOfWork : BaseUnitOfWork
    {
        public ToDoUnitOfWork(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IBaseRepositoryAsync<TodoItem> _todoRepository;
        public IBaseRepositoryAsync<TodoItem> ToDoRepository
        {
            get
            {
                if (_todoRepository == null)
                {
                    _todoRepository = new BaseRepositoryAsync<TodoItem>(_dbContext);
                }
                return _todoRepository;
            }
        }
    }
}
