namespace CardGamesAPI.Interfaces
{
    public interface IPlayer
    {
        IPlayer Opponent { get; set; }
        string Name { get; set; }
        int Score { get; set; }
        bool IsDead { get; set; }
        void Draw(int n);
    }
}
