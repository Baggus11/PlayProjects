namespace CardGamesAPI.Yugioh.Classes
{
    public class SynchroPendulumMonster : PendulumMonsterBase, ISynchroMonster
    {
        public IYugiohCard[] SynchroMaterials { get; set; }
        public int Level { get; set; }

        public SynchroPendulumMonster()
            : this(typeof(SynchroPendulumMonster).Name)
        { }

        public SynchroPendulumMonster(string name)
            : this(name, YugiohMonsterCardType.SynchroPendulum) { }

        public SynchroPendulumMonster(string name, YugiohMonsterCardType monsterCardType)
            : base(name, monsterCardType) { }

        public override void Dispose()
        {
            SynchroMaterials = null;
            base.Dispose();
        }

    }

}