using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class NormalSpellBuilder : SpellCardBuilderBase
    {
        public NormalSpellBuilder() : base(YugiohSpellCardType.Normal) { }

        public override void Build(object details)
        {
            NormalSpell derived = new NormalSpell();
            derived.Slurp(details);

            _card = derived;
        }
    }
}