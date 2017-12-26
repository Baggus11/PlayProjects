using CardGamesAPI.Yugioh.Builders;
using CardGamesAPI.Yugioh.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh.Factories
{
    //Big mamma factory for all cards
    public static class YugiohCardFactory
    {
        private static Dictionary<YugiohCardType, YugiohCardFactoryBase> baseTypes = new Dictionary<YugiohCardType, YugiohCardFactoryBase>()
        {
            [YugiohCardType.Monster] = new MonsterFactory(),
            [YugiohCardType.Spell] = new SpellFactory(),
            [YugiohCardType.Trap] = new TrapFactory(),
        };

        private static Dictionary<Type, YugiohCardFactoryBase> baseInterfaces = new Dictionary<Type, YugiohCardFactoryBase>()
        {
            [typeof(IMonsterCard)] = new MonsterFactory(),
            [typeof(ISpellCard)] = new SpellFactory(),
            [typeof(ITrapCard)] = new TrapFactory(),
        };

        public static IYugiohCardFactory GetFactory(YugiohCardType baseType) => baseTypes[baseType];

        public static IYugiohCardFactory GetFactory<T>() where T : IYugiohCard => baseInterfaces[typeof(T)];

        public static IYugiohCard CreateCard(YugiohCardType baseType) => GetFactory(baseType).CreateCard();

        public static T CreateCard<T>() where T : IYugiohCard => (T)(GetFactory<T>().CreateCard());

        public static IYugiohCard BuildCard(object details) => YugiohCardDirector.BuildCard(details);
    }
}
