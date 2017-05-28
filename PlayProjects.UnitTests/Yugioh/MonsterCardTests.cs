using CardGamesAPI.Constants;
using CardGamesAPI.Yugioh;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Common.Yugioh.Tests
{
    [TestClass()]
    public class MonsterCardTests
    {
        [TestMethod()]
        public void MonsterCardTest()
        {
            //MonsterCard cardA = new MonsterCard("BEWD", "Light", "Normal");
            //cardA.Dump();
            MonsterCard cardB = new MonsterCard("BEWD", YugiohMonsterAttribute.Light, YugiohMonsterType.Dragon, YugiohMonsterBaseType.Normal);
            cardB.Dump();
            ////Assert.Equals(cardA, cardB);
            //Assert.IsTrue(cardA.Compare(cardB));
            Assert.IsNotNull(cardB);
        }
    }
}