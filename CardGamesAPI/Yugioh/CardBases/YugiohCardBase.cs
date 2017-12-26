using System;

namespace CardGamesAPI.Yugioh
{
    public abstract partial class YugiohCardBase : IYugiohCard
    {
        public Guid SysGuid { get; set; } = Guid.NewGuid();
        public string KonamiId { get; set; }
        public string CardName { get; set; }
        public ICardEffect CardEffect { get; set; }
        public YugiohCardType CardType { get; set; }
        public YugiohCardPosition Position { get; set; } = YugiohCardPosition.AttackMode;
        public virtual int SpellSpeed { get; set; }
        public string Text { get; set; }
        public string LocalPath { get; set; }
        public string Url { get; set; }

        public YugiohCardBase(string cardName, YugiohCardType cardType)
        {
            CardName = cardName;
            CardType = cardType;
        }

    }
}
