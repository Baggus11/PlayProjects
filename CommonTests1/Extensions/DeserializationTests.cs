using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ExtractObjectTest()
        {
            string pattern = @"^(?<Name>\w+)\s(?<Age>\d+)$";
            string lineText = "Mike 28";
            var trooper = lineText.Extract<Stormtrooper>(pattern).Dump("trooper");

            Assert.IsNotNull(trooper);
        }

        [TestMethod]
        public void XmlAndJsonDeserializationToDynamicObject()
        {


        }

        [TestMethod]
        public void MapDeserializedAttributes()
        {

        }

        public class Porg
        {
            public string Name { get; set; }
            public int Wingspan { get; set; }
        }

        public class Stormtrooper
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}