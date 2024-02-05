using System.Collections.Generic;
using System.Linq.Expressions;

namespace System
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
                if (knownKeys.Add(keySelector(element)))
                    yield return element;
        }
        public static string GetMemberName<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            Expression body = expression.Body;
            return _GetMemberName(body);
        }
        public static string GetMemberName<T>(this Expression<Func<T, object>> expression)
        {
            Expression body = expression.Body;
            return _GetMemberName(body);
        }
        public static string GetMemberName(this Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            return _GetMemberName(body);
        }
        private static string _GetMemberName(this Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                    return _GetMemberName(memberExpression.Expression) + "." + memberExpression.Member.Name;
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception($"Cannot interpret member from {expression}");

                return _GetMemberName(unaryExpression.Operand);
            }

            throw new Exception($"Could not determine member from {expression}");
        }

    }
}

