using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class RitualMonsterBuilder : MonsterCardBuilderBase
    {
        public RitualMonsterBuilder() : base(YugiohMonsterCardType.Ritual)
        {
        }

        public override void Build(object details)
        {
            RitualMonster derived = new RitualMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}