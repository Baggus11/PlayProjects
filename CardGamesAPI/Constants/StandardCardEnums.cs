namespace CardGamesAPI.Constants
{
    public static class YugiohConstants
    {
        public const int YugiohMinDeckSize = 40;
        public const int YugiohMaxDeckSize = 60;
    }
    public static class StandardConstants
    {
        const int StandardDeckSize = 52;
    }
    public enum CardSuit
    {
        Spades,
        Hearts,
        Diamonds,
    }
    public enum CardFaceValue
    {
        //Numeric values according to blackjack (for now)
        Ace = 11, //or 1
        King = 10,
        Queen = 10,
        Jack = 10,
        Ten = 10,
        Nine = 9,
        Eight = 8,
        Seven = 7,
        Six = 6,
        Five = 5,
        Four = 4,
        Three = 3,
        Two = 2,
        Joker = 0,
    }
    public enum CardColor { Black, Red }
}
