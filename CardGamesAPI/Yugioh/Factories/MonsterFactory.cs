using CardGamesAPI.Yugioh.Classes;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Factories
{
    public class MonsterFactory : YugiohCardFactoryBase
    {
        public MonsterFactory()
        {
            var monsterPrototype = new MonsterCard
            {
                MonsterImplementation = new MonsterWithEffectImplementation()
            };

            cardPrototype = monsterPrototype as IEffectMonster;
        }
    }
}
