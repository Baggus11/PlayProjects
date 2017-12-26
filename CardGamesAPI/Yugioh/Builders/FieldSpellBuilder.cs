using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class FieldSpellBuilder : SpellCardBuilderBase
    {
        public FieldSpellBuilder() : base(YugiohSpellCardType.Field)
        {
        }

        public override void Build(object details)
        {
            FieldSpell derived = new FieldSpell();
            derived.Slurp(details);
            _card = derived;
        }
    }
}