using System;

namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard
    {
        event EventHandler<YugiohCardEventArgs> EffectTriggered;
        Guid SysGuid { get; }

        string KonamiID { get; set; }

        string CardName { get; set; }

        int SpellSpeed { get; set; } //for effects, obv not every card has one

        YugiohCardBaseType CardBaseType { get; set; }

        YugiohCardPosition Position { get; set; }

        string LocalPath { get; set; }

        string Url { get; set; }
    }
}
