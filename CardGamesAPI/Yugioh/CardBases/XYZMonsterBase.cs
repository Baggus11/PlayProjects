namespace CardGamesAPI.Yugioh
{
    public abstract class XYZMonsterBase : MonsterCardBase, IXYZMonster
    {
        public IYugiohCard[] XYZMaterials { get; set; }
        public int Rank { get; set; }

        public XYZMonsterBase()
            : this(typeof(XYZMonsterBase).Name)
        { }

        public XYZMonsterBase(string name)
        {
            CardName = name;
            MonsterCardType = YugiohMonsterCardType.XYZ;

        }

    }

}
