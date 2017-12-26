namespace CardGamesAPI.Yugioh
{
    public interface ISpellCard : IYugiohCard
    {
        YugiohSpellCardType SpellType { get; set; }
    }
}
