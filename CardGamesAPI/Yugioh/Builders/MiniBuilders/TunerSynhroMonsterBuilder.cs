using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class TunerSynhroMonsterBuilder : MonsterCardBuilderBase
    {
        public TunerSynhroMonsterBuilder() : base(YugiohMonsterCardType.TunerSynchro)
        {
        }

        public override void Build(object details)
        {
            TunerSynchroMonster derived = new TunerSynchroMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}