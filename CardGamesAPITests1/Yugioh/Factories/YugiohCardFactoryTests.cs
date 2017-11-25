using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CardGamesAPI.Yugioh.Tests
{
    [TestClass()]
    public class YugiohCardFactoryTests
    {
        YugiohCardFactory factory = null;

        [TestMethod]
        public void CreateSpecificCardType()
        {
            var type = EnumExtensions.GetRandomEnumValue<YugiohMonsterCardType>();
            var baseType = type.GetBaseCardType();
            factory = YugiohCardFactory.GetFactory(baseType);
            var monster = factory.CreateCard() as MonsterCardBase;
            monster.MonsterCardType = type;
            monster.Dump("specified card");

        }

        [TestMethod]
        public void CreateAllCardTypes()
        {
            try
            {
                bool showNulls = false;
                factory = YugiohCardFactory.GetFactory(YugiohCardType.Monster);

                var allMonsterTemplates = EnumExtensions.GetValues<YugiohMonsterCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                 {
                     var card = factory.CreateCard() as MonsterCardBase;
                     card.MonsterCardType = type;
                     list.Add(card);
                     return list;
                 });

                factory = YugiohCardFactory.GetFactory(YugiohCardType.Spell);
                var allSpellTemplates = EnumExtensions.GetValues<YugiohSpellCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                    {
                        var card = factory.CreateCard() as SpellCardBase;
                        card.SpellType = type;
                        list.Add(card);
                        return list;
                    });

                factory = YugiohCardFactory.GetFactory(YugiohCardType.Trap);
                var allTrapTemplates = EnumExtensions.GetValues<YugiohTrapCardType>()
                    .Aggregate(new List<IYugiohCard>(), (list, type) =>
                    {
                        var card = factory.CreateCard() as TrapCardBase;
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