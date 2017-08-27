using System;
using System.Diagnostics;

namespace CardGamesAPI
{
    public class AlphaBetaMinimax : IDisposable
    {
        const bool MaxPlayer = true;
        IMove _rootMove;

        public int Iterate(IMove move, int depth, int alpha, int beta, bool player)
        {
            Debug.WriteLine($"player: {move.TurnPlayer.Name} player score: {move.TurnPlayer.Score} ");
            Debug.WriteLine($"alpha: {alpha}, beta: {beta}");

            if (depth == 0 || move.IsTerminal(player))
            {
                return move.GetScore(player);
            }

            if (player == MaxPlayer)
            {
                foreach (var child in move.Children)
                {
                    alpha = Math.Max(alpha, Iterate(child, depth - 1, alpha, beta, !player));
                    if (beta < alpha)
                        break;

                }

                return alpha;
            }
            else
            {
                foreach (var child in move.Children)
                {
                    beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta, !player));
                    if (beta < alpha)
                        break;
                }

                return beta;
            }
        }

        public void Dispose()
        {
            _rootMove.Dispose();
            _rootMove = null;
        }

    }

}
