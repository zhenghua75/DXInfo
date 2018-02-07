namespace System.Linq.Dynamic
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class IQueryableUtil<T, PT>
    {
        public static IQueryable Contains(IQueryable Source, string PropertyName, string SearchClause)
        {
            ParameterExpression expression;
            MemberExpression expression2 = Expression.Property(expression = Expression.Parameter(typeof(T), "item"), PropertyName);
            Expression.Convert(expression2, typeof(object));
            ConstantExpression expression3 = Expression.Constant(SearchClause, typeof(string));
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Call(expression2, "Contains", new Type[0], new Expression[] { expression3 }), new ParameterExpression[] { expression });
            return Source.OfType<T>().AsQueryable<T>().Where<T>(predicate);
        }

        public static IQueryable Sort(IQueryable source, string sortExpression, bool Ascending)
        {
            ParameterExpression expression;
            Expression<Func<T, PT>> keySelector = Expression.Lambda<Func<T, PT>>(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(T), "item"), sortExpression), typeof(PT)), new ParameterExpression[] { expression });
            if (Ascending)
            {
                return source.OfType<T>().AsQueryable<T>().OrderBy<T, PT>(keySelector);
            }
            return source.OfType<T>().AsQueryable<T>().OrderByDescending<T, PT>(keySelector);
        }
    }
}

