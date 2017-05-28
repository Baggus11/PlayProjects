using CardGames;
using CardGamesAPI.Constants;
using System;
namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard : ICard
    {
        string CardTitle { get; set; }
        string KonamiID { get; }
        int? SpellSpeed { get; set; } //for effects, obv not every card has one
        YugiohCardBaseType CardType { get; set; }
        event EventHandler<YugiohCardEventArgs> EffectTriggered;
        void ChangePosition(IYugiohCard card);
    }
}
