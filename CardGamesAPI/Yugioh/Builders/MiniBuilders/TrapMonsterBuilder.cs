using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class TrapMonsterBuilder : TrapCardBuilderBase
    {
        public TrapMonsterBuilder() : base(YugiohTrapCardType.TrapMonster) { }

        public override void Build(object details)
        {
            TrapMonster derived = new TrapMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}