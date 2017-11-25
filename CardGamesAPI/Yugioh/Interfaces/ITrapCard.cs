namespace CardGamesAPI.Yugioh
{
    public interface ITrapCard : IYugiohCard
    {
        YugiohTrapCardType TrapType { get; set; }
    }
}
