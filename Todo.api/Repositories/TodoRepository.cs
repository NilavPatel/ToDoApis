using Todo.api.Models;
using Todo.api.Core;

namespace Todo.api.Repositories{
    public class TodoRepository : BaseRepositoryAsync<TodoItem>
    {   
        public TodoRepository(TodoContext dbContext) : base(dbContext)
        {
            
        }
    }
}