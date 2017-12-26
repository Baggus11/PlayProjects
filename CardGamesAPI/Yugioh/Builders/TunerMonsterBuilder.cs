using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class TunerMonsterBuilder : MonsterCardBuilderBase
    {
        public TunerMonsterBuilder() : base(YugiohMonsterCardType.Tuner)
        {
        }

        public override void Build(object details)
        {
            TunerMonster derived = new TunerMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}