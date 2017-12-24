namespace CardGamesAPI.Yugioh.Interfaces
{
    public interface IYugiohCardBuilder
    {
        void Build(object[] details);
        IYugiohCard Card { get; }
    }
}