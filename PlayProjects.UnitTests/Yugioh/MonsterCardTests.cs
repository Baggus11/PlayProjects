using CardGamesAPI.Constants;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CardGamesAPI.Yugioh.Tests
{
    [TestClass()]
    public class MonsterCardTests
    {
        [TestMethod()]
        public void MonsterCardTest()
        {
            IMonsterCard cardA = new MonsterCard("BEWD", "Light", "Normal");
            cardA.Dump();
            IMonsterCard cardB = new MonsterCard("BEWD", YugiohMonsterAttribute.Light, YugiohMonsterType.Normal);
            cardB.Dump();
            //Assert.Equals(cardA, cardB);
            Assert.IsTrue(cardA.Compare(cardB));
        }
    }
}