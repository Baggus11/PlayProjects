using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ActionExtensions
    {
        /// <summary>
        /// Emulates the 'with' functionality from Visual Basic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="work"></param>
        public static void With<T>(this T item, Action<T> work)
        {
            work(item);
        }
    }
}
