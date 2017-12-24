namespace CardGamesAPI.Yugioh
{
    public abstract class TrapCardBase : YugiohCardBase, ITrapCard
    {
        public YugiohTrapCardType TrapType { get; set; }
        public override int SpellSpeed
        {
            get => TrapType == YugiohTrapCardType.Counter ? 3 : 2;
            set => base.SpellSpeed = value;
        }

        public TrapCardBase()
            : this(typeof(ITrapCard).Name, YugiohTrapCardType.Normal, 1) { }

        public TrapCardBase(string name)
            : this(name, YugiohTrapCardType.Normal) { }

        public TrapCardBase(string name, YugiohTrapCardType trapType)
            : this(name, trapType, null) { }

        public TrapCardBase(string name, YugiohTrapCardType trapType, int? spellspeed)
            : base(name, YugiohCardType.Trap)
        {
            TrapType = trapType;
            SpellSpeed = spellspeed == null ? 2 :
                trapType == YugiohTrapCardType.Counter ? 3 : 2;

        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}
