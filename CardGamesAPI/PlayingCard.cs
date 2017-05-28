using CardGamesAPI.Constants;
namespace CardGamesAPI
{
    //poco class, may extend/modify
    public class PlayingCard
    {
        public CardSuit Suit { get; set; }
        public CardColor Color { get; set; }
        public CardFaceValue MyProperty { get; set; }
    }
}
