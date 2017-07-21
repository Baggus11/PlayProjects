using CardGamesAPI.Yugioh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayProjects.UnitTests;
using System.Diagnostics;
using System.Linq;

namespace Common.Extensions.Tests
{
    [TestClass()]
    public class TypeExtensionsTests
    {
        [TestMethod()]
        public void GetRepositoriesTest()
        {
            //var dictionary = TypeExtensions.GetRepositories<YugiohCard, IYugiohCard>();
        }

        [TestMethod]
        public void DeserializeXml_To_SimpleClass()
        {
            try
            {
                string xml = YugiohTestFixture.CreateCards()
                       .OfType<IYugiohCard>()
                       .FirstOrDefault()
                       .SerializeToXml();

                xml.Dump(nameof(xml));

                IYugiohCard card = xml.DeserializeFromXml<IYugiohCard>();
                card.Dump(nameof(card));

                Assert.IsNotNull(card);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}