using CardGamesAPI.Yugioh.Builders;

namespace CardGamesAPI.Yugioh.Interfaces
{
    public abstract class YugiohCardFactoryBase : IYugiohCardFactory
    {
        protected IYugiohCard cardPrototype;

        public virtual IYugiohCard CreateCard() => (IYugiohCard)cardPrototype.Clone();
        public virtual IYugiohCard BuildCardDetails(params object[] details) => YugiohCardDirector.BuildCard(details);
    }
}
