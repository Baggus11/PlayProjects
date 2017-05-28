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
            IMonsterCard card = new MonsterCard("BEWD", YugiohMonsterAttribute.Light);
            card.Dump();
        }
    }
}