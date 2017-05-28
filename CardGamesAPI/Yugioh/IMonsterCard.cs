namespace CardGamesAPI.Yugioh
{
    public interface IMonsterCard : IYugiohCard
    {
        YugiohMonsterType MonsterType { get; }
        YugiohMonsterAttribute MonsterAttribute { get; }
        YugiohMonsterBaseType MonsterBaseType { get; }
        int Attack { get; set; }
        int Defense { get; set; }
        int Rank { get; set; }
        int Level { get; set; }
    }
}
