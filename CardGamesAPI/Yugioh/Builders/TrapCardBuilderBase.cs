using CardGamesAPI.Yugioh.Factories;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Builders
{
    internal abstract class TrapCardBuilderBase : IYugiohCardBuilder
    {
        protected ITrapCard _card;

        public TrapCardBuilderBase(YugiohTrapCardType TrapCardType) => _card = YugiohCardFactory.CreateCard(TrapCardType.GetBaseType()) as ITrapCard;

        //public virtual void Build(object details) => _trap.Slurp(details);
        public abstract void Build(object details);

        public IYugiohCard Card => _card;

    }
}
