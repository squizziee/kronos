using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Kronos.Machina.Domain.Specifications
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        bool UseSplitQuery { get; protected set; }
        bool EnableTracking { get; protected set; }
        bool IgnoreQueryFilters { get; protected set; }
        bool EvaluateOnClientSide { get; protected set; }
        List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>> Includes { get; protected set; }
        Expression<Func<TEntity, bool>> FilterCriteria { get; protected set; }
    }
}
