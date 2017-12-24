namespace CardGamesAPI.Yugioh
{
    //Abstract because there are many types of Pendulum:http://yugioh.wikia.com/wiki/Pendulum_Monster
    public abstract class PendulumMonsterBase : MonsterCardBase, IPendulumMonster
    {
        //private YugiohMonsterCardType normalPendulum;

        public int LeftScale { get; set; }
        public int RightScale { get; set; }
        public ICardEffect PendulumEffect { get; set; }

        public PendulumMonsterBase()
            : this(typeof(PendulumMonsterBase).Name) { }

        public PendulumMonsterBase(string name)
            : this(name, YugiohMonsterCardType.Pendulum) { }

        public PendulumMonsterBase(string name, YugiohMonsterCardType type)
            : base(name, type) { }

        public override void Dispose()
        {
            CardEffect = null;
            PendulumEffect = null;
            base.Dispose();
        }

    }

}
