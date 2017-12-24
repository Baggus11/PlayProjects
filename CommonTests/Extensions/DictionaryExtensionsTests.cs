using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Common.Extensions.Tests
{
    [TestClass()]
    public class DictionaryExtensionsTests
    {
        [TestMethod()]
        public void GetValuesTest()
        {
            Farm farm = new Farm();

            //Debug.WriteLine(farm.puppies.GetValue<int>(2));
        }

        public class Farm
        {
            public Dictionary<int, string> puppies = new Dictionary<int, string>
            {
                [1] = "Corgi",
                [2] = "Poodle",
                [3] = "Bulldog",
                [4] = "Yorkie"
            };

        }

    }
}