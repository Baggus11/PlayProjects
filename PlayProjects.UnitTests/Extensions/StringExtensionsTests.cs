using CardGamesAPI.Yugioh;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PlayProjects.UnitTests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ExtractObjectTest()
        {
            string yugiohCardPattern = @"(?<CardName>\w+)";
            string lineItem = "Kuriboh";

            IYugiohCard card = lineItem.ExtractObject<IYugiohCard>(yugiohCardPattern, false);

            card.Dump();

        }

        [TestMethod()]
        public void Extract_Object_Test2()
        {
            string pattern = @"^(?<Name>\w+)";
            string lineItem = "Kuriboh";

            var person = lineItem.ExtractObject<Person>(pattern, true, true);

            var match = Regex.Match(lineItem, pattern);

            Debug.WriteLine(match.Groups["Name"].Value);

            person.Dump("person");
            //match.Dump();

        }

        [TestMethod]
        public void DeserialzieXML_To_ClassInstance()
        {
            //Assemble:
            var person = new Person { Name = "Kuriboh" };
            string xml = person.SerializeToXml();

            //Act:
            xml.Dump("xml");
            var person2 = xml.DeserializeFromXml<Person>();
            person2.Dump("person2");

            //Assert:
            Assert.IsTrue(person2.Name.Equals("Kuriboh"));
        }



    }
}