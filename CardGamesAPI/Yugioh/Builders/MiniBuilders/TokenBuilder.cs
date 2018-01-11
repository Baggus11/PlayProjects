using CardGamesAPI.Yugioh.Classes;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class TokenBuilder : MonsterCardBuilderBase
    {
        public TokenBuilder() : base(YugiohMonsterCardType.Token)
        {
        }

        public override void Build(object details)
        {
            Token derived = new Token();
            derived.Slurp(details);
            _card = derived;
        }

    }
}