using System.Linq.Expressions;

namespace core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Predicate { get; }
    List<string> Includes { get; }

    IQueryable<T> ApplyPredicate(IQueryable<T> query);
}
