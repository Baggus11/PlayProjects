namespace CardGamesAPI.Yugioh.Classes
{
    public class PendulumMonster : PendulumMonsterBase
    {
        public PendulumMonster()
            : this(typeof(PendulumMonster).Name) { }

        public PendulumMonster(string name)
            : this(name, YugiohMonsterCardType.Pendulum) { }

        public PendulumMonster(string name, YugiohMonsterCardType type)
            : base(name, type) { }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

}