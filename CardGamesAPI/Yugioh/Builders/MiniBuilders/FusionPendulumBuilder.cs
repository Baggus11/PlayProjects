using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class FusionPendulumMonsterBuilder : MonsterCardBuilderBase
    {
        public FusionPendulumMonsterBuilder() : base(YugiohMonsterCardType.FusionPendulum)
        {
        }

        public override void Build(object details)
        {
            FusionPendulumMonster derived = new FusionPendulumMonster();
            derived.Slurp(details);

            _card = derived;
        }
    }
}