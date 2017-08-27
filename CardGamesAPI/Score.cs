namespace CardGamesAPI
{
    public class Score
    {
        public Score(int playerScore, int opponentScore)
        {
            PlayerScore = playerScore;
            OpponentScore = opponentScore;
        }

        public int PlayerScore { get; set; }
        public int OpponentScore { get; set; }
    }
}
