using System;
using System.Collections.Generic;
using CardGames;
namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard(string monsterName, YugiohMonsterAttribute attribute, YugiohMonsterType type, YugiohMonsterBaseType baseType, int attack = 0, int defense = 0) : base(monsterName, attribute, type, baseType, attack, defense)
        {
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
