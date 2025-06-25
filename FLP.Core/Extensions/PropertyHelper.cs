using System.Linq.Expressions;

namespace FLP.Core.Extensions;

public static class PropertyHelper
{
    public static string GetPropertyName<T>(Expression<Func<T, object?>> expression)
    {
        if (expression.Body is MemberExpression member)
        {
            return member.Member.Name;
        }

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
        {
            return unaryMember.Member.Name;
        }

        throw new ArgumentException("Invalid expression");
    }
}
