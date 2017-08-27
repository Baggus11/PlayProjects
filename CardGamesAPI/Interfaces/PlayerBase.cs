namespace CardGamesAPI
{
    public abstract class PlayerBase : IPlayer
    {
        public int Score { get; set; }
        public bool IsDead { get { return Score <= 0; } set { } }
        public string Name { get; set; }
        public IPlayer Opponent { get; set; }

        public abstract void Draw(int n);
        public PlayerBase(string playerName)
        {
            Name = playerName;
        }

    }
    public interface IPlayer
    {
        IPlayer Opponent { get; set; }
        string Name { get; set; }
        int Score { get; set; }
        bool IsDead { get; set; }
        void Draw(int n);
    }

}
