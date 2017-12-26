namespace CardGamesAPI.Yugioh
{
    public interface IMonsterCard : IYugiohCard
    {
        YugiohMonsterType? MonsterType { get; }
        YugiohMonsterAttribute? MonsterAttribute { get; }
        YugiohMonsterCardType? MonsterCardType { get; set; }
        int Attack { get; set; }
        int Defense { get; set; }
    }
}
