using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGamesAPI.Yugioh
{
    //abstract factory for creating other card factories
    public abstract class YugiohCardFactory
    {

        //TODO: create another method that takes a list of params object[] and decides from that which type should be implemeted

        public static IYugiohCard CreateCard(string cardName, YugiohCardBaseType baseType, params object[] values)
        {
            //Using this abstract factory to create the 3 base type of factories:
            switch (baseType)
            {
                case YugiohCardBaseType.MonsterCard:
                    return MonsterFactory.CreateMonster(cardName);
                case YugiohCardBaseType.SpellCard:
                    return SpellFactory.CreateSpell(cardName);
                case YugiohCardBaseType.TrapCard:
                    return TrapFactory.CreateTrap(cardName);
                default:
                    return default(IYugiohCard);
            }

        }

        public static IYugiohCard CreateRandomCard()
        {
            //todo: pick random type and params object[]s, then create call CreateCard(name,base,values) on that.  Unrelated values will be 'sifted' or ignored.
            throw new NotImplementedException();
        }

        public static IYugiohCard CreateCardOfType<T>(string cardName)
        {
            return CreateCardOfType(typeof(T), cardName);
        }

        private static IYugiohCard CreateCardOfType(Type type, string cardName)
        {
            try
            {
                var @switch = new Dictionary<Type, Func<string, IYugiohCard>>()
                {
                    {typeof(IMonsterCard), (name) => { return new MonsterCard(name); } },
                    {typeof(ISpellCard), (name)=> { return new SpellCard(name); } },
                    {typeof(ITrapCard), (name)=> { return new TrapCard(name); } },

                };

                return @switch[type](cardName);

            }
            catch (Exception)
            {
                throw;

            }

        }

        #region Helpers

        public static IYugiohCard CreateRandomCardType()
        {
            var type = typeof(IYugiohCard).Assembly.GetTypes()
                //Only using interfaces atm.
                .Where(t => t.IsInterface)
                .Where(p => typeof(IYugiohCard).IsAssignableFrom(p))
                .TakeFirstRandom();

            return CreateRandomCard(type);

        }

        public static IYugiohCard CreateRandomCard<T>()
            where T : IYugiohCard
        {
            return CreateRandomCard(typeof(T));
        }

        public static IYugiohCard CreateRandomCard(Type type)
        {
            try
            {
                var @switch = new Dictionary<Type, Func<IYugiohCard>>()
                {
                    {typeof(IMonsterCard), ()=> { return MonsterFactory.CreateRandomMonster(); } },
                    {typeof(ISpellCard), ()=> { return SpellFactory.CreateRandomSpell(); } },
                    {typeof(ITrapCard), ()=> { return TrapFactory.CreateRandomTrap(); } },

                };

                return @switch[type]();

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Helpers

    }

    public class MonsterFactory : YugiohCardFactory
    {
        public static IYugiohCard CreateMonster(string name)
        {
            return new MonsterCard(name);
        }

        public static MonsterCard CreateRandomMonster()
        {
            int attack = Enumerable.Range(1, 30).Select(x => x * 100).TakeFirstRandom();
            int defense = Enumerable.Range(0, 25).Select(x => x * 100).TakeFirstRandom();
            var attribute = EnumExtensions.GetRandomEnumValue<YugiohMonsterAttribute>();
            var baseType = EnumExtensions.GetRandomEnumValue<YugiohMonsterBaseType>();
            var monsterType = EnumExtensions.GetRandomEnumValue<YugiohMonsterType>();
            int level = Enumerable.Range(1, 12).TakeFirstRandom();

            return new MonsterCard("Token", attribute, monsterType, baseType, level, attack, defense);

        }

    }
    public class SpellFactory : YugiohCardFactory
    {
        public static IYugiohCard CreateSpell(string name)
        {
            return new SpellCard(name);
        }

        public static SpellCard CreateRandomSpell()
        {
            var spelltype = EnumExtensions.GetRandomEnumValue<YugiohSpellType>();
            int spellspeed = Enumerable.Range(1, 3).TakeFirstRandom();

            return new SpellCard("random spell");

        }

    }

    public class TrapFactory
    {
        public static IYugiohCard CreateTrap(string name)
        {
            return new TrapCard(name);
        }

        public static TrapCard CreateRandomTrap()
        {
            return new TrapCard("random trap");
        }

    }
}
