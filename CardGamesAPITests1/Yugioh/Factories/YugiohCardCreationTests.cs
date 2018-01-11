using CardGamesAPI.Yugioh.Classes;
using CardGamesAPI.Yugioh.Factories;
using CardGamesAPI.Yugioh.Interfaces;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CardGamesAPI.Yugioh.Tests
{
    [TestClass()]
    public class YugiohCardCreationTests
    {
        [TestMethod]
        public void BuildFromDb()
        {

        }

        [TestMethod]
        public void BuildAllTypesUsingSingleDirector()
        {
            var token = YugiohCardFactory.CreateCard(YugiohCardType.Token).Dump("token");
            Assert.IsNotNull(token);

            foreach (var monsterCardType in Extensions.GetValues<YugiohMonsterCardType>())
            {
                Debug.WriteLine($"Key: {monsterCardType.ToString()}");

                object details = new
                {
                    CardType = YugiohCardType.Monster,
                    MonsterCardType = monsterCardType,
                    MonsterAttribute = YugiohMonsterAttribute.Light,
                    Attack = 300,
                    MonsterType = YugiohMonsterType.Fairy,
                    Defense = 200,
                    Level = 9,
                    SpellSpeed = 1,
                    CardName = "Winged Kuriboh Lvl 9",
                    Text = "During either player's turn, as Chain Link 3 or higher: You can Special Summon this card from your hand. Spell Cards that have been activated are banished instead of being sent to the Graveyard. The ATK and DEF of this card are each equal to the number of Spell Cards in your opponent's Graveyard x 500. You can only control 1 face-up \"Winged Kuriboh LV9\".",
                    Url = @"\nope\.avi",
                    LocalPath = @"\Decktop\",
                    KonamiId = 33776734.ToString(),
                };

                var card = YugiohCardFactory.BuildCard(details);
                card.Dump();
                Assert.IsNotNull(card);
            }

            foreach (var type in Extensions.GetValues<YugiohSpellCardType>())
            {
                object details = new
                {
                    CardType = YugiohCardType.Spell,
                    SpellType = type.Dump("spellcard type"),
                    KonamiId = 86318356.ToString(),
                    SpellSpeed = 1,
                    CardName = "Sogen",
                    Position = YugiohCardPosition.SetInAttackPosition,
                    Text = "Increases the ATK and DEF of all Beast-Warrior and Warrior-Type monsters by 200 points.",
                    Url = @"\nope\.avi",
                    LocalPath = @"\Decktop\",
                };

                var card = YugiohCardFactory.BuildCard(details);

                card.Dump();
                Assert.IsNotNull(card);
            }

            foreach (var type in Extensions.GetValues<YugiohTrapCardType>())
            {
                object details = new
                {
                    CardType = YugiohCardType.Trap,
                    TrapType = type.Dump("trap type"),
                    KonamiId = 86318356.ToString(),
                    SpellSpeed = 1,
                    CardName = "Sogen",
                    Position = YugiohCardPosition.SetFaceDown,
                    Text = "Increases the ATK and DEF of all Beast-Warrior and Warrior-Type monsters by 200 points.",
                    Url = @"\nope\.avi",
                    LocalPath = @"\Decktop\",
                };

                var card = YugiohCardFactory.BuildCard(details);
                card.Dump();
                Assert.IsNotNull(card);
            }

        }

        [TestMethod]
        public void BuildFusionMonster()
        {
            object details = new
            {
                CardType = YugiohCardType.Monster,
                MonsterCardType = YugiohMonsterCardType.Fusion,
                MonsterAttribute = YugiohMonsterAttribute.Light,
                MonsterType = YugiohMonsterType.Fairy,
                Attack = 300,
                Defense = 200,
                Level = 9,
                SpellSpeed = 1,
                CardName = "Winged Kuriboh Lvl 9",
                Text = "During either player's turn, as Chain Link 3 or higher: You can Special Summon this card from your hand. Spell Cards that have been activated are banished instead of being sent to the Graveyard. The ATK and DEF of this card are each equal to the number of Spell Cards in your opponent's Graveyard x 500. You can only control 1 face-up \"Winged Kuriboh LV9\".",
                Url = @"\nope\.avi",
                LocalPath = @"\Decktop\",
                KonamiId = 33776734.ToString(),
            };

            IFusionMonster card = (IFusionMonster)YugiohCardFactory.BuildCard(details);
            card.FusionMaterials.Add(new SpellCard("Transcendant Wings"));
            card.Dump(card.GetType().Name);
            Assert.IsNotNull(card);
            Assert.IsTrue(card is IFusionMonster);
        }

        [TestMethod]
        public void BuildFieldSpell()
        {
            object details = new
            {
                CardType = YugiohCardType.Spell,
                MonsterCardType = YugiohSpellCardType.Field,
                KonamiId = 86318356.ToString(),
                SpellSpeed = 1,
                CardName = "Sogen",
                Position = YugiohCardPosition.SetInAttackPosition,
                Text = "Increases the ATK and DEF of all Beast-Warrior and Warrior-Type monsters by 200 points.",
                Url = @"\nope\.avi",
                LocalPath = @"\Decktop\",
            };

            IFieldSpell card = (IFieldSpell)YugiohCardFactory.BuildCard(details);
            card.Dump(card.GetType().Name);
            Assert.IsNotNull(card);
            Assert.IsTrue(card is IFieldSpell);
        }

        [TestMethod]
        public void BuildCounterTrap()
        {
            object details = new
            {
                CardName = "Solemn Judgment",
                CardType = YugiohCardType.Trap,
                TrapType = YugiohTrapCardType.Counter,
                Position = YugiohCardPosition.SetFaceDown,
                KonamiId = 41420027.ToString(),
                SpellSpeed = 3,
                Text = "When a monster would be Summoned, OR a Spell/Trap Card is activated: Pay half your Life Points; negate the Summon or activation, and if you do, destroy that card.",
                Url = @"\nope\.avi",
                LocalPath = @"\Decktop\",
            };

            ICounterTrap card = (ICounterTrap)YugiohCardFactory.BuildCard(details);
            card.Dump(card.GetType().Name);
            Assert.IsNotNull(card);
            Assert.IsTrue(card is ICounterTrap);

        }

        [TestMethod]
        public void CreateBlankMonsterCardByType() => YugiohCardFactory.CreateCard(YugiohCardType.Monster).Dump();

        [TestMethod]
        public void CreateBlankMonsterCardByInterface() => YugiohCardFactory.CreateCard<IMonsterCard>().Dump();

        [TestMethod]
        public void CreateBlankSpellCardByType() => YugiohCardFactory.CreateCard(YugiohCardType.Spell).Dump();

        [TestMethod]
        public void CreateBlankSpellCardByInterface() => YugiohCardFactory.CreateCard<ISpellCard>().Dump();

        [TestMethod]
        public void CreateBlankTrapCardByInterface() => YugiohCardFactory.CreateCard<ITrapCard>().Dump();

        [TestMethod]
        public void CreateBlankTrapCardByType() => YugiohCardFactory.CreateCard(YugiohCardType.Trap).Dump();

        [TestMethod]
        public void CreateAllCardTypesByFactory()
        {
            try
            {
                bool showNulls = false;
                IYugiohCardFactory _monsterFactory = YugiohCardFactory.GetFactory(YugiohCardType.Monster);
                IYugiohCardFactory _spellFactory = YugiohCardFactory.GetFactory(YugiohCardType.Monster);
                IYugiohCardFactory _trapFactory = YugiohCardFactory.GetFactory(YugiohCardType.Monster);

                var allMonsterTemplates = Extensions.GetValues<YugiohMonsterCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                 {
                     var card = _monsterFactory.CreateCard() as IMonsterCard;
                     card.MonsterCardType = type;
                     list.Add(card);
                     return list;
                 });

                var allSpellTemplates = Extensions.GetValues<YugiohSpellCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                    {
                        var card = _spellFactory.CreateCard() as ISpellCard;
                        card.SpellType = type;
                        list.Add(card);
                        return list;
                    });

                var allTrapTemplates = Extensions.GetValues<YugiohTrapCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                    {
                        var card = _trapFactory.CreateCard() as ITrapCard;
                        card.TrapType = type;
                        list.Add(card);
                        return list;
                    });

                allMonsterTemplates.Dump("all possible monster card templates", showNulls);
                allSpellTemplates.Dump("all possible spell card templates", showNulls);
                allTrapTemplates.Dump("all possible trap card templates", showNulls);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                throw;
            }

        }

    }

}

