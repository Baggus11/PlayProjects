using System;

namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard : IDisposable, ICloneable
    {
        event EventHandler<YugiohCardEventArgs> EffectTriggered;
        int SpellSpeed { get; set; }
        Guid SysGuid { get; }

        string KonamiID { get; set; }

        string CardName { get; set; }

        string Text { get; set; }

        string Url { get; set; }

        string LocalPath { get; set; }

        YugiohCardType CardType { get; set; }

        //YugiohCardPosition Position { get; set; }

    }
}
