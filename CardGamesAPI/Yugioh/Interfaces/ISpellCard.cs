namespace CardGamesAPI.Yugioh
{
    public interface ISpellCard : IYugiohCard
    {
        YugiohSpellType SpellType { get; set; }
    }
}
