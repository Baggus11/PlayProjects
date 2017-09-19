using CardGamesAPI.Yugioh;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayProjects.UnitTests.Yugioh
{

    [TestClass]
    public class YugiohCardFactoryTests
    {
        [TestMethod]
        [TestCategory("YugiohCardFactory Tests")]
        [Description("Test for the creation of new Yugioh cards via the Factory Pattern(s)")]
        public void Can_Factory_Create_Multiple_CardTypes()
        {
            IYugiohCard trap = YugiohCardFactory.CreateCard("Trap Hole", YugiohCardBaseType.TrapCard);
            IYugiohCard monster = YugiohCardFactory.CreateCard("Neo the Magical Swordsman", YugiohCardBaseType.MonsterCard);
            IYugiohCard spell = YugiohCardFactory.CreateCard("Pot of Greed", YugiohCardBaseType.SpellCard);

            trap.Dump("trap");
            monster.Dump("monster");
            spell.Dump("spell");

            Assert.IsTrue(trap.CardType.Equals(YugiohCardBaseType.TrapCard));
            Assert.IsTrue(monster.CardType.Equals(YugiohCardBaseType.MonsterCard));
            Assert.IsTrue(spell.CardType.Equals(YugiohCardBaseType.SpellCard));
        }
    }
}
