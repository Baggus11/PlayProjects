namespace CardGamesAPI.Yugioh
{
    public interface ITrapCard : IYugiohCard
    {
        YugiohTrapType TrapType { get; set; }
    }
}
