using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dangl.Data.Shared.QueryUtilities
{
    /// <summary>
    /// This will apply a string filter to a given query by utilizing
    /// the expression. Strings may be whitespace separated and all query
    /// segments will be combined with AND / &amp;&amp;
    /// </summary>
    public static class StringFilterQueryExtensions
    {
        /// <summary>
        /// This will apply a string filter to a given query by utilizing
        /// the expression. Strings may be whitespace separated and all query
        /// segments will be combined with AND / &amp;&amp;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="filter"></param>
        /// <param name="filterExpression"></param>
        /// <param name="transformFilterToLowercase">If this is true, the filter will be transformed to lowercase</param>
        /// <returns></returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable,
            string filter,
            Func<string, Expression<Func<T, bool>>> filterExpression,
            bool transformFilterToLowercase)
        {
            queryable = queryable ?? throw new ArgumentNullException(nameof(queryable));
            filterExpression = filterExpression ?? throw new ArgumentNullException(nameof(filterExpression));

            if (string.IsNullOrWhiteSpace(filter))
            {
                return queryable;
            }

            var filterSegments = filter
                .Split(' ')
                .Select(s => transformFilterToLowercase ? s.ToLowerInvariant() : s)
                .ToList();

            if (filterSegments.Count == 1)
            {
                return queryable.Where(filterExpression(filterSegments.Single()));
            }
            else
            {
                var segments = filterSegments
                    .Select(s => filterExpression(s))
                    .ToList();

                var predicate = segments[0].And(segments[1]);

                for (var i = 2; i < segments.Count; i++)
                {
                    predicate = predicate.And(segments[i]);
                }

                return queryable.Where(predicate);
            }
        }
    }
}
