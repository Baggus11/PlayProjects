using CardGamesAPI.Constants;
using Common.Extensions;
namespace CardGamesAPI.Yugioh
{
    public abstract class MonsterCardBase : YugiohCardBase, IMonsterCard
    {
        public YugiohMonsterAttribute MonsterAttribute { get; set; }
        public YugiohMonsterType MonsterBaseType { get; set; }
        public MonsterCardBase(string monsterName)
        {
            CardTitle = monsterName;
        }
        public MonsterCardBase(string monsterName, string monsterAttribute, string monsterType)
        {
            CardTitle = monsterName;
            MonsterAttribute = monsterAttribute.ToEnum<YugiohMonsterAttribute>();
            MonsterBaseType = !string.IsNullOrWhiteSpace(monsterType) ? YugiohMonsterType.Normal : monsterType.ToEnum<YugiohMonsterType>();

        }
        public MonsterCardBase(string monsterName, YugiohMonsterAttribute monsterAttribute, YugiohMonsterType monsterBaseType = YugiohMonsterType.Normal)
        {
            CardTitle = monsterName;
            MonsterAttribute = monsterAttribute;
            MonsterBaseType = monsterBaseType;
        }
    }
}
