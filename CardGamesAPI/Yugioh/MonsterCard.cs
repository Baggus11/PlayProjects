using System;
namespace CardGamesAPI.Yugioh
{
    public class MonsterCard : MonsterCardBase
    {
        public override Guid SysGuid { get; set; }
        public MonsterCard(string monsterName, YugiohMonsterAttribute attribute, YugiohMonsterType type, YugiohMonsterBaseType baseType)
            : base(monsterName, attribute, type, baseType)
        {
            CardName = monsterName;
            MonsterAttribute = attribute;
            MonsterType = type;
            MonsterBaseType = baseType;
        }
        public override void Dispose()
        {
        }
    }
}
