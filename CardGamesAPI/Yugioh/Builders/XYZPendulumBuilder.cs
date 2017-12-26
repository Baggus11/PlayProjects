using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class XYZPendulumMonsterBuilder : MonsterCardBuilderBase
    {
        public XYZPendulumMonsterBuilder() : base(YugiohMonsterCardType.XYZPendulum)
        {
        }

        public override void Build(object details)
        {
            XYZPendulumMonster derived = new XYZPendulumMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}