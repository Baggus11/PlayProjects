using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class CounterTrapBuilder : TrapCardBuilderBase
    {
        public CounterTrapBuilder() : base(YugiohTrapCardType.Counter) { }

        public override void Build(object details)
        {
            CounterTrap derived = new CounterTrap();
            derived.Slurp(details);
            _card = derived;
        }
    }
}