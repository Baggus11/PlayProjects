namespace CardGamesAPI.Yugioh.Classes
{
    //Can still have a Pendulum Effect, however!
    public class NormalPendulumMonster : PendulumMonsterBase, INormalMonster
    {
        public int Level { get; set; }

        public NormalPendulumMonster()
            : this(typeof(NormalPendulumMonster).Name) { }

        public NormalPendulumMonster(string name)
            : this(name, YugiohMonsterCardType.NormalPendulum) { }

        public NormalPendulumMonster(string name, YugiohMonsterCardType monsterCardType)
            : base(name, monsterCardType) { }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

}