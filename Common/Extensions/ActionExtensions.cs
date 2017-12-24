using System;

namespace Common
{
    public static class ActionExtensions
    {
        public static void With<T>(this T item, Action<T> work) => work(item);
    }
}
