namespace CardGamesAPI.Yugioh
{
    public abstract class SpellCardBase : YugiohCardBase, ISpellCard
    {
        public SpellCardBase()
            : this(typeof(SpellCard).Name, YugiohSpellType.Normal, 1)
        {
            CardType = YugiohCardBaseType.SpellCard;
        }

        public SpellCardBase(string name)
            : this(name, YugiohSpellType.Normal)
        {
        }

        public SpellCardBase(string name, YugiohSpellType type)
            : this(name, type, null)
        {
        }

        public SpellCardBase(string name, YugiohSpellType type, int? spellspeed)
        {
            CardType = YugiohCardBaseType.SpellCard;

            CardName = name;
            SpellType = type;

            if (spellspeed == null)
            {
                switch (type)
                {
                    //source:http://yugioh.wikia.com/wiki/Spell_Speed_2
                    case YugiohSpellType.QuickPlay:
                        SpellSpeed = 2;
                        break;
                    default:
                        SpellSpeed = 1;
                        break;
                }

            }

        }

        public YugiohSpellType SpellType { get; set; }

    }

}
