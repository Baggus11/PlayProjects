namespace CardGamesAPI
{
    /// <summary>
    /// My algorithms controller will iterate through moves of any kind.
    /// Moves will have a related effect service that can operate on other moves/objects in a game
    /// Moves otherwise just hold state
    /// </summary>
    public interface ICardGameMove
    {
        IGameState MoveState { get; set; } //Same as a Gamestate, just named differently.  
        IEffectService EffectService { get; }//this service will run the proper effects for a given game.
        //Other move properties...
    }
}
