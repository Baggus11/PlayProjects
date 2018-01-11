using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class EquipSpellBuilder : SpellCardBuilderBase
    {
        public EquipSpellBuilder() : base(YugiohSpellCardType.Equip)
        {
        }

        public override void Build(object details)
        {
            EquipSpell derived = new EquipSpell();
            derived.Slurp(details);

            _card = derived;
        }
    }
}