namespace CardGamesAPI.Yugioh
{
    public interface IEffectMonster : IYugiohCard
    {
        ICardEffect CardEffect { get; set; }
        int SpellSpeed { get; set; } //http://www.yu-gi-oh-cards.net/howtoplay/yugioh-chains.html
    }
}