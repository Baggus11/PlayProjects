namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// May be difficult to program as it has dual functionality in game
    /// </summary>
    public abstract class TrapMonsterCardBase : TrapCardBase, IMonsterCard
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public YugiohMonsterAttribute MonsterAttribute { get; }
        public YugiohMonsterBaseType MonsterBaseType { get; }
        public YugiohMonsterType MonsterType { get; }
    }
}
