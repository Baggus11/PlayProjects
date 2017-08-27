using Common;
using System;
using System.Diagnostics;
using System.Linq;

namespace CardGamesAPI.Yugioh
{
    public static class YugiohCardFactory
    {
        public static IYugiohCard CreateBasicYugiohCard<T>()
            where T : IYugiohCard
        {
            try
            {
                var baseType = typeof(T).Name.ToEnum<YugiohCardBaseType>();

                switch (baseType)
                {
                    case YugiohCardBaseType.MonsterCard:
                        return CreateBasicMonster();
                    case YugiohCardBaseType.SpellCard:
                        return CreateBasicSpell();
                    case YugiohCardBaseType.TrapCard:
                        return CreateBasicTrap();
                    default:
                        throw new Exception("No Card could be created!");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private static MonsterCard CreateBasicMonster()
        {
            int attack = Enumerable.Range(1, 30).Select(x => x * 100).GetFirstRandom();
            int defense = Enumerable.Range(0, 25).Select(x => x * 100).GetFirstRandom();
            var attribute = EnumExtensions.GetRandomEnumValue<YugiohMonsterAttribute>();
            var baseType = EnumExtensions.GetRandomEnumValue<YugiohMonsterBaseType>();
            var monsterType = EnumExtensions.GetRandomEnumValue<YugiohMonsterType>();
            int level = Enumerable.Range(1, 12).GetFirstRandom();

            var result = new MonsterCard("Token", attribute, monsterType, baseType, level, attack, defense);

            return result;

        }

        private static SpellCard CreateBasicSpell()
        {
            throw new NotImplementedException();
        }

        private static TrapCard CreateBasicTrap()
        {
            throw new NotImplementedException();
        }
    }

}

