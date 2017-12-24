namespace CardGamesAPI.Yugioh.Classes
{
    public class FusionMonster : FusionMonsterBase
    {
        public FusionMonster() : this(typeof(FusionMonster).Name) { }

        public FusionMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Fusion;
        }
    }
}