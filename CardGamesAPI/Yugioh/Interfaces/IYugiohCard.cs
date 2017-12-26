using System;

namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard : IDisposable, ICloneable
    {
        Guid SysGuid { get; }

        event EventHandler<YugiohCardEventArgs> EffectTriggered;

        //http://www.yu-gi-oh-cards.net/howtoplay/yugioh-chains.html
        int SpellSpeed { get; set; }

        string KonamiId { get; set; }

        string CardName { get; set; }

        string Text { get; set; }

        string Url { get; set; }

        string LocalPath { get; set; }

        YugiohCardType CardType { get; set; }

        YugiohCardPosition Position { get; set; }
    }
}
