namespace CardGamesAPI.Yugioh.Interfaces
{
    public interface IYugiohCardFactory
    {
        IYugiohCard CreateCard();
        IYugiohCard BuildCardDetails(params object[] details);
    }
}