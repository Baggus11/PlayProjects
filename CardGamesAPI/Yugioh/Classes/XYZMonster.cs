namespace CardGamesAPI.Yugioh
{
    internal class XYZMonster : XYZMonsterBase, IMonsterCard
    {
        public XYZMonster()
            : base()
        { }

        public XYZMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.XYZ;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}