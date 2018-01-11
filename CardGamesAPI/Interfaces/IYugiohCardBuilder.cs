namespace CardGamesAPI.Yugioh.Interfaces
{
    public interface IYugiohCardBuilder
    {
        void Build<T>(object details);
        IYugiohCard Card { get; }
    }
}