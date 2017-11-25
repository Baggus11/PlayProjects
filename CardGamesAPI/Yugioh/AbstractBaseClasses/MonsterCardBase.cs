namespace CardGamesAPI.Yugioh
{
    public abstract class MonsterCardBase : YugiohCardBase, IMonsterCard
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

        public YugiohMonsterAttribute? MonsterAttribute { get; set; }
        public YugiohMonsterType? MonsterType { get; set; }
        public YugiohMonsterCardType? MonsterCardType { get; set; }

        public MonsterCardBase()
            : this(typeof(IMonsterCard).Name, YugiohMonsterCardType.Normal) { }

        public MonsterCardBase(string monsterName)
            : this(monsterName, YugiohMonsterCardType.Normal) { }

        public MonsterCardBase(string monsterName, YugiohMonsterCardType monsterCardType)
            : base(monsterName, YugiohCardType.Monster)
        {
            MonsterCardType = monsterCardType;
            //Attack = attack; //Build these with a builder pattern
            //Defense = defense;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}
