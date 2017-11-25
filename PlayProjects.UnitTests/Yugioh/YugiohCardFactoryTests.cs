using CardGamesAPI.Yugioh;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PlayProjects.UnitTests.Yugioh
{

    [TestClass]
    public class YugiohCardFactoryTests
    {

        [TestMethod]
        public void Can_Create_Basic_Instances_of_Card_Types()
        {
            var cards = new List<IYugiohCard>()
            {
                //YugiohCardFactory.CreateCard("FrightFur Leo", YugiohMonsterCardType.Fusion),
                //YugiohCardFactory.CreateCard("Relinquished", YugiohMonsterCardType.Ritual),
                //YugiohCardFactory.CreateCard("Stardust Dragon", YugiohMonsterCardType.Synchro),
                //YugiohCardFactory.CreateCard("Number 39: Utopia", YugiohMonsterCardType.XYZ),
                //YugiohCardFactory.CreateCard("Dark Resonator", YugiohMonsterCardType.Tuner),
                //YugiohCardFactory.CreateCard("Gemini Soldier", YugiohMonsterCardType.Gemini),
                //YugiohCardFactory.CreateCard("Trap Master", YugiohMonsterCardType.FlipEffect),
                //YugiohCardFactory.CreateCard("Kuriboh", YugiohMonsterCardType.Effect),
                //YugiohCardFactory.CreateCard("Odd-Eyes Pendulum Dragon", YugiohMonsterCardType.Pendulum),
                //YugiohCardFactory.CreateCard("Red Eyes Black Dragon", YugiohMonsterCardType.Normal),

                //YugiohCardFactory.CreateCard("Pot of Greed", YugiohSpellBaseType.Normal),
                //YugiohCardFactory.CreateCard("Black Garden", YugiohSpellBaseType.Field),
                //YugiohCardFactory.CreateCard("The Monarchs Stormforth", YugiohSpellBaseType.QuickPlay),
                //YugiohCardFactory.CreateCard("Final Countdown", YugiohSpellBaseType.Continuous),
                //YugiohCardFactory.CreateCard("Axe of Despair", YugiohSpellBaseType.Equip),
                //YugiohCardFactory.CreateCard("Black Illusion Ritual", YugiohSpellBaseType.Ritual),

                //YugiohCardFactory.CreateCard("Solemn Wishes", YugiohTrapBaseType.Continuous),
                //YugiohCardFactory.CreateCard("Seven Tools of the Bandit", YugiohTrapBaseType.Counter),
                //YugiohCardFactory.CreateCard("Trap Hole", YugiohTrapBaseType.Normal),
                //YugiohCardFactory.CreateCard("Metal Reflect Slime", YugiohTrapBaseType.TrapMonster),

            };
            cards.Dump();

        }

        [TestMethod]
        [TestCategory("YugiohCardFactory Tests")]
        [Description("Test for the creation of new Yugioh cards via the Factory Pattern(s)")]
        public void Can_Factory_Create_Multiple_CardTypes()
        {
            //IYugiohCard monster = YugiohCardFactory.CreateCard("Neo the Magical Swordsman");
            //IYugiohCard trap = YugiohCardFactory.CreateCard("Trap Hole");
            //IYugiohCard spell = YugiohCardFactory.CreateCard("Pot of Greed");

            //trap.Dump("trap");
            //monster.Dump("monster");
            //spell.Dump("spell");

            //Assert.IsTrue(trap.CardType.Equals(YugiohCardType.TrapCard));
            //Assert.IsTrue(monster.CardType.Equals(YugiohCardType.MonsterCard));
            //Assert.IsTrue(spell.CardType.Equals(YugiohCardType.SpellCard));

        }

        [TestMethod]
        [TestCategory("YugiohCardFactory Tests")]
        public void Can_Factory_Create_All_CardTypes_From_Anonymous_Params()
        {

        }

    }
}
