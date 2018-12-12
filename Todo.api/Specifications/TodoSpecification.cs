using Todo.api.Core;
using Todo.api.Models;
using System.Linq.Expressions;

namespace Todo.api.Specifications{
    public class TodoSpecification : BaseSpecification<TodoItem>
    {
        public TodoSpecification(Expression<System.Func<TodoItem, bool>> criteria) : base(criteria)
        {
        }
    }
}