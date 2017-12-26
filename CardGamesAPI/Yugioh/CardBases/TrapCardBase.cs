namespace CardGamesAPI.Yugioh
{
    public abstract class TrapCardBase : YugiohCardBase, ITrapCard
    {
        public YugiohTrapCardType TrapType { get; set; }

        public TrapCardBase() : this(typeof(ITrapCard).Name, YugiohTrapCardType.Normal) { }

        public TrapCardBase(string name) : this(name, YugiohTrapCardType.Normal) { }

        public TrapCardBase(string name, YugiohTrapCardType trapType)
            : base(name, YugiohCardType.Trap)
        {
            TrapType = trapType;

        }

        //public TrapCardBase(string name, YugiohTrapCardType trapType, int? spellspeed)
        //    : base(name, YugiohCardType.Trap)
        //{
        //    //SpellSpeed = spellspeed == null ? 2 :
        //    //    trapType == YugiohTrapCardType.Counter ? 3 : 2;
        //}

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}
