using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Common.Extensions.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void CacheTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Extensions.Cache(typeof(Person));
            //Extensions.PropertyCache[typeof(Person)].Dump("property cache");

            int iterations = 50;
            var list = Enumerable.Repeat(new Person { Name = "Mike", Age = 28 }, iterations).ToList();

            list.ToDatatable()//.Dump("table")
            .ToList<Person>()//.Dump("list")
            .ToDatatable()//.Dump("table again")
            .ToList<Person>()/*.Dump("list again")*/;
            watch.Stop();
            Debug.WriteLine("Time Elapsed: " + watch.Elapsed);
        }

        //[TestMethod]
        //public void RandomNumbersTest()
        //{
        //    var random = new Random();
        //    var randomNumbers = Enumerable
        //        .Repeat(0, 1000)
        //        .Select(_ => random.NextDouble());

        //    randomNumbers.Dump("");
        //}
    }
}