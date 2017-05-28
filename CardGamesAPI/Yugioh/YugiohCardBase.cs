using System;
using CardGames;
using CardGamesAPI.Constants;
namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// This class will be the base class for ALL Yugioh Cards'.
    /// </summary>
    public abstract class YugiohCardBase : IYugiohCard
    {
        public string CardTitle { get; set; }
        public string KonamiID { get; set; }
        public YugiohCardBaseType CardType { get; set; }
        public int? SpellSpeed { get; set; }
        public event EventHandler<YugiohCardEventArgs> EffectTriggered;
        public void Flip()
        {
            throw new NotImplementedException();
        }
        public void Set()
        {
            throw new NotImplementedException();
        }
        public abstract bool MyRequiredCardMethod1();
        public abstract void Dispose();
        protected virtual void RaiseEffectTriggered(YugiohCardEventArgs e) => EffectTriggered?.Invoke(this, e);
        public void ChangePosition(IYugiohCard card)
        {
            throw new NotImplementedException();
        }
    }
}
