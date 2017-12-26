namespace CardGamesAPI.Interfaces
{
    public abstract class Player : IPlayer
    {
        public int Score { get; set; }
        public bool IsDead { get { return Score <= 0; } set { } }
        public string Name { get; set; }
        public IPlayer Opponent { get; set; }

        public abstract void Draw(int n);
        public Player(string playerName)
        {
            Name = playerName;
        }

    }

}
