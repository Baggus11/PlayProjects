namespace CardGamesAPI
{
    public interface ICheatChecker //just an idea, not sure how to implement well.
    {
        IGameState State { get; }

        bool RunCheck(IGameState state);

    }
}
