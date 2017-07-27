using System.Collections.Generic;
using System.Threading;

namespace Common
{
    public static class ThreadExtensions
    {
        public static void WaitAll(this IEnumerable<Thread> threads)
        {
            if (threads != null)
            {
                foreach (Thread thread in threads)
                { thread.Join(); }
            }
        }
    }
}
