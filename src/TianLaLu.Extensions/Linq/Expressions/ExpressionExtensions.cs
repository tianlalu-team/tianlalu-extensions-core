using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace System.Linq.Expressions
{
    [DebuggerStepThrough]
    public static class ExpressionExtensions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static PropertyInfo GetPropertyAccess(this LambdaExpression propertyAccessExpression)
        {
            var parameterExpression = propertyAccessExpression.Parameters.Single();
            var propertyInfo = parameterExpression.MatchSimplePropertyAccess(propertyAccessExpression.Body);
            
            if (propertyInfo == null)
                throw new ArgumentException($"The expression '{nameof (propertyAccessExpression)}' is not a valid property expression. The expression should represent a simple property access: 't =>; t.MyProperty'.");
            
            var declaringType = propertyInfo.DeclaringType;
            var type = parameterExpression.Type;
            
            if (declaringType != null && declaringType != type && (declaringType.GetTypeInfo().IsInterface && declaringType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo())))
            {
                var propertyGetter = propertyInfo.GetMethod;
                var runtimeInterfaceMap = type.GetTypeInfo().GetRuntimeInterfaceMap(declaringType);
                var targetMethod = runtimeInterfaceMap.TargetMethods[Array.FindIndex(runtimeInterfaceMap.InterfaceMethods, p => propertyGetter.Equals(p))];
                foreach (var runtimeProperty in type.GetRuntimeProperties())
                {
                    if (targetMethod.Equals(runtimeProperty.GetMethod))
                        return runtimeProperty;
                }
            }
            
            return propertyInfo;
        }
        
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        private static PropertyInfo MatchSimplePropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
        {
            var propertyInfoList = parameterExpression.MatchPropertyAccess(propertyAccessExpression);
            if (propertyInfoList == null || propertyInfoList.Count != 1)
                return null;
            return propertyInfoList[0];
        }

        private static IReadOnlyList<PropertyInfo> MatchPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
        {
            var propertyInfoList = new List<PropertyInfo>();
            MemberExpression memberExpression;
            do
            {
                memberExpression = propertyAccessExpression.RemoveConvert().RemoveTypeAs() as MemberExpression;
                var member = memberExpression?.Member as PropertyInfo;
                if (member == null)
                    return null;
                propertyInfoList.Insert(0, member);
                propertyAccessExpression = memberExpression.Expression;
            }
            while (memberExpression.Expression.RemoveConvert().RemoveTypeAs() != parameterExpression);
            return propertyInfoList;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static Expression RemoveConvert(this Expression expression)
        {
            while (expression != null && (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked))
                expression = ((UnaryExpression) expression).Operand.RemoveConvert();
            return expression;
        }
        
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static Expression RemoveTypeAs(this Expression expression)
        {
            while (expression != null && expression.NodeType == ExpressionType.TypeAs)
                expression = ((UnaryExpression) expression).Operand.RemoveConvert();
            return expression;
        }
    }
}