namespace CardGamesAPI.Yugioh
{
    public abstract partial class SpellCardBase : YugiohCardBase, ISpellCard
    {
        public YugiohSpellCardType SpellType { get; set; }
        public override int SpellSpeed
        {
            get => SpellType == YugiohSpellCardType.QuickPlay ? 2 : 1;
            set => base.SpellSpeed = value;
        }

        public SpellCardBase()
            : this(typeof(ISpellCard).Name, YugiohSpellCardType.Normal, 1) { }

        public SpellCardBase(string name)
            : this(name, YugiohSpellCardType.Normal) { }

        public SpellCardBase(string name, YugiohSpellCardType spellType)
            : this(name, spellType, null) { }

        public SpellCardBase(string name, YugiohSpellCardType spellType, int? spellspeed)
            : base(name, YugiohCardType.Spell) => SpellType = spellType;

        public override void Dispose() => base.Dispose();

    }

}
