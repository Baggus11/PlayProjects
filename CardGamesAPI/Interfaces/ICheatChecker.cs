namespace CardGamesAPI
{
    public interface ICheatChecker //just an idea, not sure how to implement well.
    {
        IMove MoveState { get; }

        bool RunCheck(IMove state);

    }
}
