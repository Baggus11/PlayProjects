using System;
namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// This class will be the base class for ALL Yugioh Cards.
    /// </summary>
    public abstract class YugiohCardBase : IYugiohCard
    {
        public string CardName { get; set; }
        public string KonamiID { get; set; }
        public YugiohCardBaseType CardBaseType { get; set; }
        public int SpellSpeed { get; set; }
        public abstract Guid SysGuid { get; set; }

        public event EventHandler<YugiohCardEventArgs> EffectTriggered;
        public void Flip()
        {
            throw new NotImplementedException();
        }
        public void Set()
        {
            throw new NotImplementedException();
        }
        public abstract void Dispose();
        protected virtual void RaiseEffectTriggered(YugiohCardEventArgs e) => EffectTriggered?.Invoke(this, e);
        public void ChangePosition(IYugiohCard card)
        {
            throw new NotImplementedException();
        }
    }
}
