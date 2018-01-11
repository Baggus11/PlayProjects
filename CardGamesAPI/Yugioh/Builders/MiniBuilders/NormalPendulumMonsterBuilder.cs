using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class NormalPendulumMonsterBuilder : MonsterCardBuilderBase
    {
        public NormalPendulumMonsterBuilder() : base(YugiohMonsterCardType.NormalPendulum)
        {
        }

        public override void Build(object details)
        {
            NormalPendulumMonster derived = new NormalPendulumMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}