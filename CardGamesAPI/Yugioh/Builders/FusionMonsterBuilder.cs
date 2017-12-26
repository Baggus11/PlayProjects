using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class FusionMonsterBuilder : MonsterCardBuilderBase
    {
        public FusionMonsterBuilder() : base(YugiohMonsterCardType.Fusion) { }

        public override void Build(object details)
        {
            FusionMonster derived = new FusionMonster();
            derived.Slurp(details);

            _card = derived;
        }
    }
}
