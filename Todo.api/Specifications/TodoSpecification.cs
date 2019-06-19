using Todo.api.Core;
using Todo.api.Models;

namespace Todo.api.Specifications
{
    public class TodoSpecification : BaseSpecification<TodoItem>
    {
        public TodoSpecification(string name, int skip, int take) : base(t => t.Name.Contains(name))
        {
            ApplyOrderBy(t => t.Name);
            ApplyPaging(skip, take);
        }
    }
}