using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class QuickPlaySpellBuilder : SpellCardBuilderBase
    {
        public QuickPlaySpellBuilder() : base(YugiohSpellCardType.QuickPlay)
        {
        }

        public override void Build(object details)
        {
            QuickPlaySpell derived = new QuickPlaySpell();
            derived.Slurp(details);
            _card = derived;
        }
    }
}