using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dangl.Data.Shared.QueryUtilities
{
    /// <summary>
    /// Taken from: http://www.albahari.com/nutshell/predicatebuilder.aspx
    /// This static utility class is used to construct predicates and LINQ expressions
    /// </summary>
    internal static class PredicateBuilderExtensions
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return _ => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return _ => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
