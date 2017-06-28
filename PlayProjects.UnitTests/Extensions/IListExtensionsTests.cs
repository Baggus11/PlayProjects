using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Common.Extensions.Tests
{
    [TestClass()]
    public class IListExtensionsTests
    {
        [TestMethod()]
        public void RemoveDuplicatesTest()
        {
            List<int> duplicatesList = new List<int> { 1, 2, 1, 0, 4, 5 };
            var distinctList = duplicatesList.RemoveDuplicates(); //todo: make this work inline

            distinctList.Dump();
            duplicatesList.Dump();

            Assert.IsTrue(duplicatesList.Count == distinctList.Count);
        }
    }
}