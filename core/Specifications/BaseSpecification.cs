using System.Linq.Expressions;
using core.Interfaces;

namespace core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? predicate) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Predicate => predicate;
    public List<string> Includes { get; } = [];

    public Expression<Func<T, object>>? OrderByAscending { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    protected void AddInclude(string includeString)
    {
        Includes.Add(includeString);
    }

    protected void AddOrderByAscending(Expression<Func<T, object>> orderByAscendingExpression)
    {
        OrderByAscending = orderByAscendingExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public IQueryable<T> ApplyPredicate(IQueryable<T> query)
    {
        if (Predicate is not null)
        {
            query = query.Where(Predicate);
        }
        return query;
    }
}
