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

        public MonsterCardBase()
            : this(typeof(MonsterCard).Name, YugiohMonsterAttribute.None,
                  YugiohMonsterType.None, YugiohMonsterBaseType.None)
        {
        }

        public MonsterCardBase(string monsterName, YugiohMonsterAttribute attribute, YugiohMonsterType type,
            YugiohMonsterBaseType baseType, int level = 0, int attack = 0, int defense = 0)
                : base()
        {
            CardType = YugiohCardBaseType.MonsterCard;

            CardName = monsterName;
            MonsterAttribute = attribute;
            MonsterBaseType = baseType;
            MonsterType = type;
            Attack = attack;
            Defense = defense;
            Level = level;
        }

    }

}
