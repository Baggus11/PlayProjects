using System;

namespace CardGamesAPI.Yugioh
{    /// <summary>
     /// This class will be the base class for ALL Yugioh Cards.
     /// </summary>
    public abstract partial class YugiohCardBase : IYugiohCard
    {
        public Guid SysGuid { get; set; } = Guid.NewGuid();
        public string KonamiID { get; set; }
        public string CardName { get; set; }
        public ICardEffect CardEffect { get; set; }
        public YugiohCardType CardType { get; set; }
        public YugiohCardPosition Position { get; set; }
        public virtual int SpellSpeed { get; set; }
        public string Text { get; set; }
        public string LocalPath { get; set; } // Add with builder pattern
        public string Url { get; set; } //Add with builder pattern

        public YugiohCardBase(string cardName, YugiohCardType cardType)
        {
            CardName = cardName;
            CardType = cardType;
        }
    }
}
