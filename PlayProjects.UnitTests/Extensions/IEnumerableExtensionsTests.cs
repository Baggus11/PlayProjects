using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Common.Tests
{
    [TestClass()]
    public class IEnumerableExtensionsTests
    {
        [TestMethod()]
        public void BatchTest()
        {
            Enumerable.Range(1, 256).Batch(50).Dump("Batches");
        }
    }
}