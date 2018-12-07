using Todo.api.Repositoris;

namespace Todo.api.Models{
    public class TodoItem: BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}