namespace CardGamesAPI.Yugioh
{
    public interface IMonsterCard : IYugiohCard
    {
        YugiohMonsterType? MonsterType { get; }
        YugiohMonsterAttribute? MonsterAttribute { get; }
        YugiohMonsterCardType? MonsterCardType { get; }
        int Attack { get; set; }
        int Defense { get; set; }
        //int Rank { get; set; }
        //int Level { get; set; }
        string Text { get; set; } //can be text or effect text (handled once specified)

    }
}
