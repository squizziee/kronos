using Kronos.Machina.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Kronos.Machina.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> items, 
            ISpecification<T> specification) where T : class
        {
            var result = items;

            if (!specification.EnableTracking)
            {
                result = result.AsNoTracking();
            }

            if (specification.IgnoreQueryFilters)
            {
                result = result.IgnoreQueryFilters();
            }

            result = specification.Includes
                .Aggregate(result, (current, include) => include(current));

            if (specification.UseSplitQuery)
            {
                result = result.AsSplitQuery();
            }

            if (specification.EvaluateOnClientSide)
            {
                result = result
                    .AsEnumerable()
                    .Where(_ => true)
                    .AsQueryable();
            }

            result = result.Where(specification.FilterCriteria);

            return result;
        }
    }
}
