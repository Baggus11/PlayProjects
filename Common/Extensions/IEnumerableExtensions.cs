using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int maxBatchSize)
        {
            return items.Select((item, index) => new { item, index })
                .GroupBy(pairs => pairs.index / maxBatchSize)
                .Select(mapped => mapped.Select(pair => pair.item));
        }

        public static string ToCsv<T>(this IEnumerable<T> items)
           where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            foreach (T item in items)
            {
                string line = string.Join(",", properties
                    .Select(property => property
                        .GetValue(item, null)
                        .ToCsvValue())
                    .ToArray());

                csvBuilder.AppendLine(line);
            }

            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }

        public static void RunActionOn<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                return;

            var cached = collection;

            foreach (var item in cached)
            {
                action(item);
            }
        }

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> collection, int count)
        {
            return collection.OrderBy(c => Guid.NewGuid()).Take(count);
        }

        public static T GetFirstRandom<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
        }

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

        public static IEnumerable<T> GetWhere<T>(this IEnumerable<T> collection, LambdaExpression lambda)
        {
            try
            {
                var compiledLambda = lambda.Compile();
                return collection.Where(x => (bool)compiledLambda.DynamicInvoke(x));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> expression, int numDesired = 1)
        {
            try
            {
                Func<T, bool> funcWhere = expression.Compile();
                return collection.Where(funcWhere).Take(numDesired);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetElementsWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> where)
        {
            try
            {
                Func<T, bool> funcWhere = where.Compile();
                return collection.Where(funcWhere);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetRandomElementsWhere<T>(this IEnumerable<T> collection, Expression<Func<T, bool>> whereclause, int count)
        {
            try
            {
                Func<T, bool> funcWhere = whereclause.Compile();
                return collection.Where(funcWhere).OrderBy(c => Guid.NewGuid()).Take(count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> collection, int count)
        {
            try
            {
                return collection.OrderBy(c => Guid.NewGuid()).Take(count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        * The following are Linq Join Extensions
        * Source: https://www.codeproject.com/articles/488643/linq-extended-joins
        */

        public static IEnumerable<TResult> LeftJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> sourceCollection, IEnumerable<TInner> innerCollection,
                  Func<TSource, TKey> pk, Func<TInner, TKey> fk,
                  Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();
            _result = from s in sourceCollection
                      join i in innerCollection
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);
            return _result;
        }

        public static IEnumerable<TResult>
        RightJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> source, IEnumerable<TInner> inner,
                  Func<TSource, TKey> pk, Func<TInner, TKey> fk,
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

        public static IEnumerable<TResult> FullOuterJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> sourceCollection, IEnumerable<TInner> inner,
                  Func<TSource, TKey> pk, Func<TInner, TKey> fk,
                  Func<TSource, TInner, TResult> result)
        {
            var left = sourceCollection.LeftJoin(inner, pk, fk, result).ToList();
            var right = sourceCollection.RightJoin(inner, pk, fk, result).ToList();
            return left.Union(right);
        }

        public static IEnumerable<TResult> LeftExcludingJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> source, IEnumerable<TInner> inner,
                  Func<TSource, TKey> pk, Func<TInner, TKey> fk,
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

        public static IEnumerable<TResult> RightExcludingJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> source, IEnumerable<TInner> inner,
                  Func<TSource, TKey> pk, Func<TInner, TKey> fk,
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

        public static IEnumerable<TResult> FullOuterExcludingJoin<TSource, TInner, TKey, TResult>
            (this IEnumerable<TSource> sourceCollection, IEnumerable<TInner> innerCollection,
                 Func<TSource, TKey> pk, Func<TInner, TKey> fk,
                 Func<TSource, TInner, TResult> result)
        {
            var left = sourceCollection.LeftExcludingJoin(innerCollection, pk, fk, result).ToList();
            var right = sourceCollection.RightExcludingJoin(innerCollection, pk, fk, result).ToList();
            return left.Union(right);
        }
    }
}
