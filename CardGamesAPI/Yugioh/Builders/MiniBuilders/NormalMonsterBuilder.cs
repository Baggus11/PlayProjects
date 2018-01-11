using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class NormalMonsterBuilder : MonsterCardBuilderBase
    {
        public NormalMonsterBuilder() : base(YugiohMonsterCardType.Normal)
        {
        }

        public override void Build(object details)
        {
            NormalMonster derived = new NormalMonster();
            derived.Slurp(details);

            _card = derived;
        }
    }
}