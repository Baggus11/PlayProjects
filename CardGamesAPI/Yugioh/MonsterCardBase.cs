using System;
namespace CardGamesAPI.Yugioh
{
    public abstract class MonsterCardBase : YugiohCardBase, IMonsterCard
    {
        public YugiohMonsterAttribute MonsterAttribute { get; set; }
        public YugiohMonsterType MonsterType { get; set; }
        public YugiohMonsterBaseType MonsterBaseType { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Rank { get; set; }
        public int Level { get; set; }
        public MonsterCardBase(string monsterName, YugiohMonsterAttribute attribute, YugiohMonsterType type,
            YugiohMonsterBaseType baseType, int attack = 0, int defense = 0)
        {
            SysGuid = Guid.NewGuid();
            CardName = monsterName;
            MonsterAttribute = attribute;
            MonsterBaseType = baseType;
            MonsterType = type;
            Attack = attack;
            Defense = defense;
        }
        //public MonsterCardBase(string monsterName, string attribute, string type, string baseType = "Normal")
        //{
        //SysGuid = Guid.NewGuid();
        //    CardTitle = monsterName;
        //    MonsterAttribute = attribute.ToEnum<YugiohMonsterAttribute>();
        //    MonsterBaseType = baseType.ToEnum<YugiohMonsterBaseType>();
        //    MonsterType = type.ToEnum<YugiohMonsterType>();
        //}
    }
}
