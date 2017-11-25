namespace CardGamesAPI.Yugioh
{
    public class FusionMonster : FusionMonsterBase
    {
        public FusionMonster()
            : this(typeof(FusionMonster).Name)
        { }

        public FusionMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Fusion;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }

}