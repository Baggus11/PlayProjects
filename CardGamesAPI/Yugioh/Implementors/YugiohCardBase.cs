using System;

namespace CardGamesAPI.Yugioh
{
    public abstract partial class YugiohCardBase
    {
        public event EventHandler<YugiohCardEventArgs> EffectTriggered;
        //public abstract void OnEffectTriggered(YugiohCardEventArgs args);  //TODO: uncomment and implement this!
        //protected YugiohCardBaseImplementation _baseImplementation = new BasicCardImplementation();
        //public void SetBaseImplementation(YugiohCardBaseImplementation value) => _baseImplementation = value;

        object ICloneable.Clone()
        {
            //return Clone();
            return MemberwiseClone();
        }

        //public virtual IYugiohCard Clone()
        //{
        //    return _baseImplementation.Clone();
        //}

        public virtual void Dispose()
        {
            CardName = null;
            CardEffect = null;
            KonamiID = null;
            LocalPath = null;
            Text = null;
            Url = null;
        }
    }
}