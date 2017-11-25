using System;

namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// This class will be the base class for ALL Yugioh Cards.
    /// </summary>
    public abstract class YugiohCardBase : IYugiohCard
    {
        public Guid SysGuid { get; set; } = Guid.NewGuid();
        public string KonamiID { get; set; }
        public string CardName { get; set; }
        public ICardEffect CardEffect { get; set; }
        public YugiohCardType CardType { get; set; }
        public virtual int SpellSpeed { get; set; }
        public string Text { get; set; }
        public string LocalPath { get; set; } //obsolete - Add with pattern
        public string Url { get; set; } //obsolete - Add with pattern
        public event EventHandler<YugiohCardEventArgs> EffectTriggered;

        protected virtual void RaiseEffectTriggered(YugiohCardEventArgs e) => EffectTriggered?.Invoke(this, e);

        public YugiohCardBase(string cardName, YugiohCardType cardType)
        {
            CardName = cardName;
            CardType = cardType;
        }

        public virtual void Dispose()
        {
            CardName = null;
            CardEffect = null;
            KonamiID = null;
            LocalPath = null;
            Text = null;
            Url = null;

        }

        public object Clone()
        {
            var clone = (IYugiohCard)this.MemberwiseClone();
            HandleCloned(clone);
            return clone;
        }

        protected virtual void HandleCloned(IYugiohCard clone)
        {
            //todo: handle Token/Slime cases here.
        }
    }

}
