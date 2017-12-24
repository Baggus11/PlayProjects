namespace CardGamesAPI.Yugioh.Classes
{
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