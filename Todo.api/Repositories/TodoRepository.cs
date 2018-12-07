using Todo.api.Models;

namespace Todo.api.Repositoris{
    public class TodoRepository : BaseRepository<TodoItem>
    {   
        public TodoRepository(TodoContext dbContext) : base(dbContext)
        {
            
        }
    }
}