using CardGamesAPI.Constants;
using Common.Extensions;
using System;
namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard(string monsterName, YugiohMonsterAttribute monsterAttribute, YugiohMonsterType monsterBaseType = YugiohMonsterType.Normal) : base(monsterName, monsterAttribute, monsterBaseType)
        {
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
