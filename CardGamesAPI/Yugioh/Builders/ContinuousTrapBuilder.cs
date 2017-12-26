namespace CardGamesAPI.Yugioh.Builders
{
    internal class ContinuousTrapBuilder : TrapCardBuilderBase
    {
        public ContinuousTrapBuilder() : base(YugiohTrapCardType.Continuous) { }

        public override void Build(object details)
        {
            ContinuousTrap derived = new ContinuousTrap();
            derived.Slurp(details);
            _card = derived;
        }
    }
}