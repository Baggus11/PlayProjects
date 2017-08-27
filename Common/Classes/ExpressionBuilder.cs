using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
    public abstract class ExpressionBuilderBase : IExpressionBuilder
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


        public bool CheckRule<T>(T instance, string conditions)
        {
            if (instance == null)
            {
                throw new NullReferenceException($"{typeof(T).Name} instance");
            }

            //return CheckRule(instance, new IRule rule);
            return (bool)BuildLambdaExpression<T>(conditions).Compile().DynamicInvoke(instance);
        }
        public bool CheckRule<T>(T instance, IRule rule) where T : class
        {
            if (instance == null)
            {
                throw new NullReferenceException($"{typeof(T).Name} instance");
            }

            return (bool)BuildLambdaExpression<T>(rule.Conditions).Compile().DynamicInvoke(instance);

        }

        public IEnumerable<T> CheckRule<T>(IEnumerable<T> collection, string conditions)
        {
            try
            {
                var lambda = BuildLambdaExpression<T>(conditions);
                return Extensions.Where(collection, lambda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> CheckRule<T>(IEnumerable<T> collection, IRule rule)
        {
            try
            {
                var lambda = BuildLambdaExpression<T>(rule.Conditions);
                return Extensions.Where(collection, lambda);
                //return collection.Where(x => (bool)compiledLambda.DynamicInvoke(x));
            }
            catch (Exception)
            {
                throw;
            }

        }

    }

    public static class Extensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, LambdaExpression conditions)
        {
            try
            {
                var compiledLambda = conditions.Compile();
                return collection.Where(x => (bool)compiledLambda.DynamicInvoke(x));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
