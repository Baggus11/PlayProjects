using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Common
{
    public static class ThreadExtensions
    {
        public static void WaitAll(this IEnumerable<Thread> threads)
        {
            foreach (Thread thread in threads ?? Enumerable.Empty<Thread>())
            { thread.Join(); }
        }
    }
}
