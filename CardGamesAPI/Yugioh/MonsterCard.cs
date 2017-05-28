using CardGamesAPI.Constants;
using System;
namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard(string monsterName, string monsterAttribute, string monsterBaseType = "Normal") : base(monsterName, monsterAttribute, monsterBaseType)
        {
        }
        public MonsterCard(string monsterName, YugiohMonsterAttribute monsterAttribute, YugiohMonsterType monsterBaseType = YugiohMonsterType.Normal) : base(monsterName, monsterAttribute, monsterBaseType)
        {
            CardTitle = monsterName;
            MonsterAttribute = monsterAttribute;
            MonsterBaseType = monsterBaseType;
        }
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        public override bool MyRequiredCardMethod1()
        {
            throw new NotImplementedException();
        }
    }
}
