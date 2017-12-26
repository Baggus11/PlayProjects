using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class SynchroPendulumMonsterBuilder : MonsterCardBuilderBase
    {
        public SynchroPendulumMonsterBuilder() : base(YugiohMonsterCardType.SynchroPendulum)
        {
        }

        public override void Build(object details)
        {
            SynchroPendulumMonster derived = new SynchroPendulumMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}