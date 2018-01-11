using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class ContinuousSpellBuilder : SpellCardBuilderBase
    {
        public ContinuousSpellBuilder() : base(YugiohSpellCardType.Continuous) { }

        public override void Build(object details)
        {
            ContinuousSpell derived = new ContinuousSpell();
            derived.Slurp(details);

            _card = derived;
        }
    }
}