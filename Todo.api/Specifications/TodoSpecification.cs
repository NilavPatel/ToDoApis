using Todo.api.Core;
using Todo.api.Models;

namespace Todo.api.Specifications
{
    public class TodoSpecification
    {
        public static BaseSpecification<TodoItem> NameStartWith(string name, int skip, int take)
        {
            var spec = new BaseSpecification<TodoItem>(t => t.Name.Contains(name));
            spec.ApplyOrderBy(t => t.Name);
            spec.ApplyPaging(skip, take);
            return spec;
        }
    }
}