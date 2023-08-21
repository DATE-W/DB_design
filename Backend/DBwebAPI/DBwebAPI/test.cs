using System;
using System.Linq.Expressions;

namespace DBwebAPI
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static class SortingExpressions
    {
        public static Expression<Func<T, object>> BuildNameCharacterCountOrderByExpression<T>(char targetCharacter)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression nameProperty = Expression.Property(parameter, "Name");

            // 构建包含给定字符数量的表达式
            MethodCallExpression countMethod = Expression.Call(
                nameProperty,
                typeof(string).GetMethod("Count", new[] { typeof(char) }),
                Expression.Constant(targetCharacter)
            );

            // 转换为 object 类型
            UnaryExpression countAsObject = Expression.Convert(countMethod, typeof(object));

            Expression<Func<T, object>> orderByExpression = Expression.Lambda<Func<T, object>>(countAsObject, parameter);
            return orderByExpression;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var orderByExpression = SortingExpressions.BuildNameCharacterCountOrderByExpression<Person>('a');

            // 使用构建的排序表达式在 LINQ 查询中排序
            // 例如：var sortedPeople = people.OrderBy(orderByExpression);
        }
    }
}
/*
 using System;
using System.Linq.Expressions;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public static class SortingExpressions
{
    public static Expression<Func<T, object>> BuildCustomOrderByExpression<T>(Func<T, int> sortingFunc)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

        MethodCallExpression sortingMethod = Expression.Call(Expression.Constant(null, typeof(T)), sortingFunc.Method, parameter);

        Expression<Func<T, object>> orderByExpression = Expression.Lambda<Func<T, object>>(sortingMethod, parameter);
        return orderByExpression;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var orderByExpression = SortingExpressions.BuildCustomOrderByExpression<Person>(person => person.Age);

        // 使用构建的排序表达式在 LINQ 查询中排序
        // 例如：var sortedPeople = people.OrderBy(orderByExpression);
    }
}

 * */
