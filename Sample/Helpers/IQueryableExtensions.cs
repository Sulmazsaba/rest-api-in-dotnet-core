using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query,
                                              string sortField,
                                              SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
                return query.OrderBy(s => s.GetType()
                                           .GetProperty(sortField));
            return query.OrderByDescending(s => s.GetType()
                                                 .GetProperty(sortField));

        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
