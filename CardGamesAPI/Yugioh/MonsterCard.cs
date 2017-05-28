using CardGamesAPI.Constants;
using Common.Extensions;
using System;
namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard(string monsterName) : base(monsterName)
        {
            MonsterBaseType = "Effect/Dragon".ToEnum<YugiohMonsterType>();
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
