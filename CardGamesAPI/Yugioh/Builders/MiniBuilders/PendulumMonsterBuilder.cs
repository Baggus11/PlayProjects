namespace CardGamesAPI.Yugioh.Builders
{
    internal class PendulumMonsterBuilder : MonsterCardBuilderBase
    {
        public PendulumMonsterBuilder() : base(YugiohMonsterCardType.Pendulum) { }

        public override void Build(object details)
        {
            throw new System.NotImplementedException();
        }
    }
}