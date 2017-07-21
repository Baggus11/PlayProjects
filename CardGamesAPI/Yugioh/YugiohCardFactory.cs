using Common.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using static Common.Extensions.EnumExtensions;

namespace CardGamesAPI.Yugioh
{
    public static class YugiohCardFactory
    {
        public static IYugiohCard CreateBasicYugiohCard<T>()
            where T : IYugiohCard
            //where T : YugiohCardBase
            //where T : struct
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

            return new MonsterCard("Kuriboh",
                GetRandomEnumValue<YugiohMonsterAttribute>(),
                GetRandomEnumValue<YugiohMonsterType>(),
                GetRandomEnumValue<YugiohMonsterBaseType>(),
                attack, defense);
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

