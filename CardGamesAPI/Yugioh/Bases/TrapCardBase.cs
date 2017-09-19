namespace CardGamesAPI.Yugioh
{
    public abstract class TrapCardBase : YugiohCardBase, ITrapCard
    {
        public TrapCardBase()
            : this(typeof(SpellCard).Name, YugiohTrapType.Normal, 1)
        {
        }

        public TrapCardBase(string name)
            : this(name, YugiohTrapType.Normal)
        {
        }

        public TrapCardBase(string name, YugiohTrapType type)
            : this(name, type, null)
        {
        }

        public TrapCardBase(string name, YugiohTrapType type, int? spellspeed)
        {
            CardType = YugiohCardBaseType.TrapCard;

            CardName = name;
            TrapType = type;

            if (spellspeed == null)
            {
                switch (type)
                {
                    //source:http://yugioh.wikia.com/wiki/Spell_Speed_3
                    case YugiohTrapType.Normal:
                    case YugiohTrapType.Continuous:
                    case YugiohTrapType.TrapMonster:
                        SpellSpeed = 2;
                        break;
                    case YugiohTrapType.Counter:
                        SpellSpeed = 3;
                        break;
                }

            }

        }

        public YugiohTrapType TrapType { get; set; }
    }
}
