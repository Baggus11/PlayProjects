namespace CardGamesAPI.Yugioh.Classes
{
    public class XYZMonster : XYZMonsterBase, IMonsterCard
    {
        public XYZMonster() : base() { }

        public XYZMonster(string name) : base(name) => MonsterCardType = YugiohMonsterCardType.XYZ;

        public override void Dispose() => base.Dispose();
    }
}