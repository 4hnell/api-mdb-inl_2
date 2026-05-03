using core.Entities;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> CreateQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Predicate is not null)
        {
            query = query.Where(spec.Predicate);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
