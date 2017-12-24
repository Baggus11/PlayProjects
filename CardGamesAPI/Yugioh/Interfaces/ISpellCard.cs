namespace CardGamesAPI.Yugioh
{
    public interface ISpellCard : IYugiohCard
    {
        YugiohSpellCardType SpellType { get; set; }
        //int SpellSpeed { get; set; }
        //void Activate();
    }
}
