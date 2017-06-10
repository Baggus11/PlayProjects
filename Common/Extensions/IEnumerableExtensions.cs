using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Extensions
{
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// iterates through an IEnumerable<T> 
        /// and applies an Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) return;
            var cached = collection;
            foreach (var item in cached)
                action(item);
        }

        /// <summary>
        /// Take random elements from a IEnumerable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> collection, int count)
        {
            return collection.OrderBy(c => Guid.NewGuid()).Take(count);
        }

        /// <summary>
        /// Take random elements from a IEnumerable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T TakeFirstRandom<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
        }

        /// <summary>
        /// Move Up
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public static IEnumerable<T> MoveUp<T>(this IEnumerable<T> enumerable, int itemIndex)
        {
            int i = 0;
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (itemIndex.Equals(i))
                {
                    T previous = enumerator.Current;
                    if (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                    yield return previous;
                    break;
                }
                yield return enumerator.Current;
            }
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Get Elements Where
        /// - Gets elements from an IEnumerable those that match the (compiled) lambda expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetElementsWhere<T>(this IEnumerable<T> collection, LambdaExpression lambda)
        {
            try
            {
                var compiledLambda = lambda.Compile();
                return collection.Where(x => (bool)compiledLambda.DynamicInvoke(x));
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return collection;
            }
        }

        /// <summary>
        /// Get Where
        /// Gets N elements from an IEnumerable those that match the 'where' expression
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="where"></param>
        /// <param name="numDesired"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetElementsWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> where, int numDesired = 1)
        {
            Func<T, bool> funcWhere = where.Compile();
            return collection.Where(funcWhere).Take(numDesired);
        }

        /// <summary>
        /// Get Where
        /// Gets elements from an IEnumerable those that match the 'where' expression
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="where"></param>
        /// <param name="numDesired"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetElementsWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> where)
        {
            Func<T, bool> funcWhere = where.Compile();
            return collection.Where(funcWhere);
        }

        /// <summary>
        /// Take random elements from a IEnumerable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandomElementsWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> whereclause, int count)
        {
            Func<T, bool> funcWhere = whereclause.Compile();
            return collection.Where(funcWhere).OrderBy(c => Guid.NewGuid()).Take(count);
        }

        /// <summary>
        /// Take random elements from a IEnumerable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> collection, int count)
        {
            return collection.OrderBy(c => Guid.NewGuid()).Take(count);
        }

        //
        /// The following are Linq Join Extensions
        /// Source: https://www.codeproject.com/articles/488643/linq-extended-joins
        ////
        /// <summary>
        /// Left Join:
        /// </summary>
        public static IEnumerable<TResult>
        LeftJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                    IEnumerable<TInner> inner,
                                                    Func<TSource, TKey> pk,
                                                    Func<TInner, TKey> fk,
                                                    Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();
            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);
            return _result;
        }
        /// <summary>
        /// Right Join:
        /// </summary>
        public static IEnumerable<TResult>
        RightJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                  IEnumerable<TInner> inner,
                                                  Func<TSource, TKey> pk,
                                                  Func<TInner, TKey> fk,
                                                  Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();
            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      select result(right, i);
            return _result;
        }
        /// <summary>
        /// Full Outer Join:
        /// </summary>
        public static IEnumerable<TResult>
        FullOuterJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                          IEnumerable<TInner> inner,
                                                          Func<TSource, TKey> pk,
                                                          Func<TInner, TKey> fk,
                                                          Func<TSource, TInner, TResult> result)
        {
            var left = source.LeftJoin(inner, pk, fk, result).ToList();
            var right = source.RightJoin(inner, pk, fk, result).ToList();
            return left.Union(right);
        }
        /// <summary>
        /// Left Excluding Join:
        /// </summary>
        public static IEnumerable<TResult>
        LeftExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                          IEnumerable<TInner> inner,
                                                          Func<TSource, TKey> pk,
                                                          Func<TInner, TKey> fk,
                                                          Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();
            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      where left == null
                      select result(s, left);
            return _result;
        }
        /// <summary>
        /// Right Excluding Join:
        /// </summary>
        public static IEnumerable<TResult>
        RightExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                        IEnumerable<TInner> inner,
                                                        Func<TSource, TKey> pk,
                                                        Func<TInner, TKey> fk,
                                                        Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();
            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      where right == null
                      select result(right, i);
            return _result;
        }
        /// <summary>
        /// Full Outer Excluding Join:
        /// </summary>
        public static IEnumerable<TResult>
        FullOuterExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                      IEnumerable<TInner> inner,
                                                      Func<TSource, TKey> pk,
                                                      Func<TInner, TKey> fk,
                                                      Func<TSource, TInner, TResult> result)
        {
            var left = source.LeftExcludingJoin(inner, pk, fk, result).ToList();
            var right = source.RightExcludingJoin(inner, pk, fk, result).ToList();
            return left.Union(right);
        }
    }
}
