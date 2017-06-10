using Common.Interfaces;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
namespace Common.Classes
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        public LambdaExpression BuildLambdaExpression<T>(string conditions)
        {
            try
            {
                Type type = typeof(T);
                var paramExpr = Expression.Parameter(type, type.Name);
                var result = System.Linq.Dynamic.DynamicExpression.ParseLambda(new ParameterExpression[] { paramExpr }, null, conditions);
                return result;
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Debug.WriteLine(errMsg);
                throw;
            }
        }
    }
}
