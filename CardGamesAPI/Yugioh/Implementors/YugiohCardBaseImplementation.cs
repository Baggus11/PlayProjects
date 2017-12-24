namespace CardGamesAPI.Yugioh
{
    public abstract class YugiohCardBaseImplementation
    {
        protected IYugiohCard _implementedCard;

        public YugiohCardBaseImplementation(IYugiohCard card)
        {
            _implementedCard = card;
        }

        public virtual IYugiohCard Clone()
        {
            return _implementedCard;
        }

    }
}