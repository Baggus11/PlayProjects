using CardGamesAPI.Yugioh.Factories;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Builders
{
    internal abstract class SpellCardBuilderBase : IYugiohCardBuilder
    {
        protected ISpellCard _card;

        public SpellCardBuilderBase(YugiohSpellCardType spellCardType) => _card = YugiohCardFactory.CreateCard(spellCardType.GetBaseType()) as ISpellCard;

        //public virtual void Build(object details) => _spell.Slurp(details);
        public abstract void Build(object details);

        public IYugiohCard Card => _card;

        private void SetSpellSpeed(int spellSpeed) => _card.SpellSpeed = spellSpeed;

        protected void SetSpellCardType(YugiohSpellCardType spellCardType) => _card.SpellType = spellCardType;

    }
}
