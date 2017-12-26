namespace CardGamesAPI.Yugioh.Classes
{
    public class FieldSpell : SpellCardBase, IFieldSpell
    {
        public FieldSpell() : this(typeof(FieldSpell).Name) { }

        public FieldSpell(string name) : base(name) => SpellType = YugiohSpellCardType.Field;
    }
}