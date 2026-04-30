using System.Linq.Expressions;
using core.Interfaces;

namespace core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? predicate) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Predicate => predicate;
    public List<string> Includes { get; } = [];

    protected void AddInclude(string includeString)
    {
        Includes.Add(includeString);
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
