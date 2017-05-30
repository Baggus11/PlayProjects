using System;
namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// This class will be the base class for ALL Yugioh Cards.
    /// </summary>
    public abstract class YugiohCardBase : CardEffectBase, IYugiohCard
    {
        //Properties:
        public string CardName { get; set; }
        public string KonamiID { get; set; }
        public YugiohCardBaseType CardBaseType { get; set; }
        public YugiohCardPosition Position { get; set; }
        public int SpellSpeed { get; set; }
        public Guid SysGuid { get; set; }
        //Events:
        public event EventHandler<YugiohCardEventArgs> EffectTriggered;
        //Methods:        
        public abstract void Dispose();
        protected virtual void RaiseEffectTriggered(YugiohCardEventArgs e) => EffectTriggered?.Invoke(this, e);
        public void SetKonamiID(string konamiId)
        {
            KonamiID = konamiId;
        }
    }
}
