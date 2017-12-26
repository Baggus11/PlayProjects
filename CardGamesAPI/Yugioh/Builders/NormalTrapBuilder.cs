using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class NormalTrapBuilder : TrapCardBuilderBase
    {
        public NormalTrapBuilder() : base(YugiohTrapCardType.Normal) { }

        public override void Build(object details)
        {
            NormalTrap derived = new NormalTrap();
            derived.Slurp(details);
            _card = derived;
        }
    }
}