using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public abstract class RuleBase : IRule
    {
        public List<Action> Behaviours { get; set; }
        public abstract string Conditions { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public bool IsMatchingType<T>() => Type == typeof(T).Name;

        public bool RunBehaviours<T>(T instance)
        {
            if (!IsMatchingType<T>())
            {
                throw new Exception($"{typeof(T).Name} does not match stored type {Type}!");
            }

            foreach (var behaviour in Behaviours as List<Action<T>>)
            {
                behaviour(instance);
            }

            return true;
        }

        public bool RunActionOn<T>(IEnumerable<T> collection, Action<T> action)
        {
            if (!IsMatchingType<T>())
            {
                throw new Exception($"{typeof(T).Name} does not match stored type {Type}!");
            }

            if (collection?.Count() == 0)
            {
                return false;
            }

            var cached = collection;

            foreach (var item in cached)
            {
                action(item);
            }
            return true;

        }

        private static bool TryExecuteAction<T>(ref T item, Action<T> work)
        {
            try
            {
                work(item);
            }
            catch (Exception)
            {
                throw;
            }

            return true;

        }

    }
}
