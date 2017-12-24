using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Implementors
{
    public class TokenImplementation : YugiohCardBaseImplementation
    {
        public TokenImplementation()
            : base(new YugiohCard()) { }

        public override IYugiohCard Clone()
        {
            var clone = (IYugiohCard)MemberwiseClone();
            HandleCloned(clone);
            return clone;
        }

        protected virtual void HandleCloned(IYugiohCard clone)
        {
            //todo: handle Token/Slime cases here.
        }
    }

}
