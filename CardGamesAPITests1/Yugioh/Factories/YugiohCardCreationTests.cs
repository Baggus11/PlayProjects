using CardGamesAPI.Yugioh.Classes;
using CardGamesAPI.Yugioh.Factories;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh.Tests
{
    [TestClass()]
    public class YugiohCardCreationTests
    {
        [TestMethod]
        public void CreateBlankMonsterCardByType() => YugiohCardFactory.CreateCard(YugiohCardType.Monster).Dump();
        [TestMethod]
        public void CreateBlankMonsterCardByInterface() => YugiohCardFactory.CreateCard<IMonsterCard>().Dump();
        [TestMethod]
        public void CreateBlankSpellCardByType() => YugiohCardFactory.CreateCard(YugiohCardType.Spell).Dump();
        [TestMethod]
        public void CreateBlankSpellCardByInterface() => YugiohCardFactory.CreateCard<ISpellCard>().Dump();
        [TestMethod]
        public void CreateBlankTrapCardByType() => YugiohCardFactory.CreateCard<ITrapCard>().Dump();

        [TestMethod]
        public void BuildNewFusionMonster()
        {
            object details = new
            {
                Attack = 100,
                Defense = 2000,
                CardType = YugiohCardType.Monster,
                MonsterType = YugiohMonsterCardType.Fusion,
                Materials = new List<IYugiohCard> { new MonsterCard(), }
            };

            var card = YugiohCardFactory.BuildCard(details);


            //var details = new object[]
            //{
            //    new { Attack = 100 },
            //    new { Defense = 2000 },
            //    new { CardType = YugiohCardType.Monster },
            //    new {MonsterType = YugiohMonsterCardType.Fusion },
            //    new {Materials = new List<IYugiohCard> { new MonsterCard() }, }
            //};

            //var card = YugiohCardFactory.BuildCard(details);

            card.Dump("built card");
        }


        //[TestMethod]
        //public void CreateBlankFusionMonster() => _monsterFactory.CreateCard(YugiohMonsterCardType.Fusion);
        //[TestMethod]
        //public void CreateAllCardTypesByFactory()
        //{
        //    try
        //    {
        //        bool showNulls = false;
        //        _monsterFactory = YugiohCardFactoryBase.GetFactory(YugiohCardType.Monster);

        //        var allMonsterTemplates = EnumExtensions.GetValues<YugiohMonsterCardType>()
        //            .Aggregate(new List<IYugiohCard>(), (list, type) =>
        //         {
        //             var card = _monsterFactory.CreateCard() as MonsterCardBase;
        //             card.MonsterCardType = type;
        //             list.Add(card);
        //             return list;
        //         });

        //        var allSpellTemplates = EnumExtensions.GetValues<YugiohSpellCardType>()
        //            .Aggregate(new List<IYugiohCard>(), (list, type) =>
        //            {
        //                var card = _spellFactory.CreateCard() as SpellCardBase;
        //                card.SpellType = type;
        //                list.Add(card);
        //                return list;
        //            });

        //        var allTrapTemplates = EnumExtensions.GetValues<YugiohTrapCardType>()
        //            .Aggregate(new List<IYugiohCard>(), (list, type) =>
        //            {
        //                var card = _trapFactory.CreateCard() as TrapCardBase;
        //                card.TrapType = type;
        //                list.Add(card);
        //                return list;
        //            });

        //        allMonsterTemplates.Dump("all possible monster card templates", showNulls);
        //        allSpellTemplates.Dump("all possible spell card templates", showNulls);
        //        allTrapTemplates.Dump("all possible trap card templates", showNulls);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
        //        throw;
        //    }

        //}

    }





}