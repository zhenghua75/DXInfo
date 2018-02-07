namespace System.Linq.Dynamic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class DynamicQueryable
    {
        public static bool Any(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return (bool) source.Provider.Execute(Expression.Call(typeof(Queryable), "Any", new Type[] { source.ElementType }, new Expression[] { source.Expression }));
        }

        public static int Count(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return (int) source.Provider.Execute(Expression.Call(typeof(Queryable), "Count", new Type[] { source.ElementType }, new Expression[] { source.Expression }));
        }

        public static Type GetDataItemType(this IQueryable DataSource)
        {
            Type type = DataSource.GetType();
            Type type2 = typeof(object);
            if (type.HasElementType)
            {
                return type.GetElementType();
            }
            if (type.IsGenericType)
            {
                return type.GetGenericArguments()[0];
            }
            if (DataSource != null)
            {
                IEnumerator enumerator = DataSource.GetEnumerator();
                if (enumerator.MoveNext() && (enumerator.Current != null))
                {
                    type2 = enumerator.Current.GetType();
                }
            }
            return type2;
        }

        public static int GetTotalRowCount(this IQueryable Source)
        {
            Type type = Source.GetType();
            Type dataItemType = Source.GetDataItemType();
            return (int) typeof(IQueryableUtil<>).MakeGenericType(new Type[] { dataItemType }).GetMethod("Count", new Type[] { type }).Invoke(null, new object[] { Source });
        }

        public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (elementSelector == null)
            {
                throw new ArgumentNullException("elementSelector");
            }
            LambdaExpression expression = DynamicExpression.ParseLambda(source.ElementType, null, keySelector, values);
            LambdaExpression expression2 = DynamicExpression.ParseLambda(source.ElementType, null, elementSelector, values);
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "GroupBy", new Type[] { source.ElementType, expression.Body.Type, expression2.Body.Type }, new Expression[] { source.Expression, Expression.Quote(expression), Expression.Quote(expression2) }));
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            return (IQueryable<T>) source.OrderBy(ordering, values);
        }

        public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (ordering == null)
            {
                throw new ArgumentNullException("ordering");
            }
            ParameterExpression[] parameters = new ParameterExpression[] { Expression.Parameter(source.ElementType, "") };
            IEnumerable<DynamicOrdering> enumerable = new ExpressionParser(parameters, ordering, values).ParseOrdering();
            Expression expression = source.Expression;
            string str = "OrderBy";
            string str2 = "OrderByDescending";
            foreach (DynamicOrdering ordering2 in enumerable)
            {
                expression = Expression.Call(typeof(Queryable), ordering2.Ascending ? str : str2, new Type[] { source.ElementType, ordering2.Selector.Type }, new Expression[] { expression, Expression.Quote(Expression.Lambda(ordering2.Selector, parameters)) });
                str = "ThenBy";
                str2 = "ThenByDescending";
            }
            return source.Provider.CreateQuery(expression);
        }

        public static IQueryable Select(this IQueryable source, string selector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            LambdaExpression expression = DynamicExpression.ParseLambda(source.ElementType, null, selector, values);
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Select", new Type[] { source.ElementType, expression.Body.Type }, new Expression[] { source.Expression, Expression.Quote(expression) }));
        }

        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new Type[] { source.ElementType }, new Expression[] { source.Expression, Expression.Constant(count) }));
        }

        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new Type[] { source.ElementType }, new Expression[] { source.Expression, Expression.Constant(count) }));
        }

        public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            LambdaExpression expression = DynamicExpression.ParseLambda(source.ElementType, typeof(bool), predicate, values);
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[] { source.ElementType }, new Expression[] { source.Expression, Expression.Quote(expression) }));
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
        {
            return (IQueryable<T>) source.Where(predicate, values);
        }
    }
}

