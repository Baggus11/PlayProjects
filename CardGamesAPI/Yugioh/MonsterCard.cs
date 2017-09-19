using System;

namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard()
            : base()
        {
        }

        public MonsterCard(string name)
        {
            CardName = name;
        }

        public MonsterCard(string monsterName, YugiohMonsterAttribute attribute, YugiohMonsterType type, YugiohMonsterBaseType baseType, int level = 0, int attack = 0, int defense = 0)
            : base(monsterName, attribute, type, baseType, level, attack, defense)
        {
        }

        public override YugiohMove Activate(YugiohMove currentState)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
