using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Todo.api.Core{
    public abstract class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = 
                                           new List<Expression<Func<T, object>>>();

    public List<string> IncludeStrings { get; } = new List<string>();

        Expression<Func<T, bool>> ISpecification<T>.Criteria => throw new NotImplementedException();

        List<Expression<Func<T, object>>> ISpecification<T>.Includes => throw new NotImplementedException();

        List<string> ISpecification<T>.IncludeStrings => throw new NotImplementedException();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    // string-based includes allow for including children of children
    // e.g. Basket.Items.Product
    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}
}