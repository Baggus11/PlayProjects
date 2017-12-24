using System;
using System.Collections.Generic;

namespace CardGamesAPI.Interfaces
{

    public interface IMove : IDisposable
    {
        IPlayer TurnPlayer { get; set; }
        IPlayer Opponent { get; }
        int TurnNumber { get; set; }
        List<IMove> Children { get; set; }

        bool IsTerminal(bool player);
        int GetScoreOf(IPlayer player);
        int GetScore(bool turnPlayer);

    }

}
