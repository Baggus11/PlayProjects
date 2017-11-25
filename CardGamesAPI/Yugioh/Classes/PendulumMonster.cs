namespace CardGamesAPI.Yugioh
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

    public class FusionPendulumMonster : PendulumMonsterBase, IFusionMonster
    {
        public IYugiohCard[] FusionMaterials { get; set; }
        public int Level { get; set; }

        public FusionPendulumMonster()
            : this(typeof(FusionPendulumMonster).Name) { }

        public FusionPendulumMonster(string name)
            : this(name, YugiohMonsterCardType.FusionPendulum) { }

        public FusionPendulumMonster(string name, YugiohMonsterCardType monsterCardType)
            : base(name, monsterCardType) { }

        public override void Dispose()
        {
            FusionMaterials = null;
            base.Dispose();
        }

    }

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

    public class XYZPendulumMonster : PendulumMonsterBase, IXYZMonster
    {
        public IYugiohCard[] XYZMaterials { get; set; }
        public int Rank { get; set; }

        public XYZPendulumMonster()
            : this(typeof(XYZPendulumMonster).Name)
        { }

        public XYZPendulumMonster(string name)
            : this(name, YugiohMonsterCardType.XYZPendulum) { }

        public XYZPendulumMonster(string name, YugiohMonsterCardType monsterCardType)
            : base(name, monsterCardType) { }

        public override void Dispose()
        {
            XYZMaterials = null;
            base.Dispose();
        }

    }

}