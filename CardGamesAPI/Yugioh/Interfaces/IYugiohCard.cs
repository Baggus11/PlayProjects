using System;

namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard : ICardEffect
    {
        event EventHandler<YugiohCardEventArgs> EffectTriggered;
        Guid SysGuid { get; }

        string KonamiID { get; set; }

        string CardName { get; set; }

        int SpellSpeed { get; set; } //for effects, obv not every card has one

        YugiohCardBaseType CardType { get; set; }

        YugiohCardPosition Position { get; set; }

    }
}
