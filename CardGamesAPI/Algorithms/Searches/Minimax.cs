using CardGamesAPI.Interfaces;
using System;

namespace CardGamesAPI.Algorithms
{
    public class AlphaBetaMinimax
    {
        const bool MaxPlayer = true;

        public int Iterate(IMove move, int depth, int alpha, int beta, bool player)
        {
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

        public AlphaBetaMinimax()
        {

        }

        ~AlphaBetaMinimax()
        {

        }

    }

    //public class AlphaBeta
    //{
    //    private const bool MaxPlayer = true;

    //    public int Iterate(IMove node, int depth, int alpha, int beta, bool player)
    //    {
    //        if (depth == 0 || node.IsTerminal(player))
    //        {
    //            return node.GetScore(player);
    //        }
    //        if (player == MaxPlayer)
    //        {
    //            foreach (var child in node.Children(player))
    //            {
    //                alpha = Math.Max(alpha, Iterate(child, depth - 1, alpha, beta, !player));
    //                if (beta < alpha)
    //                {
    //                    break;
    //                }
    //            }
    //            return alpha;
    //        }
    //        foreach (var child in node.Children(player))
    //        {
    //            beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta, !player));
    //            if (beta < alpha)
    //            {
    //                break;
    //            }
    //        }
    //        return beta;
    //    }
    //}

}

namespace Algorithms
{
    public class AlphaBetaMinimax
    {

        private const bool MaxPlayer = true;

        public AlphaBetaMinimax()
        {

        }

        ~AlphaBetaMinimax()
        {

        }

        /// 
        /// <param name="move"></param>
        /// <param name="depth"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="player"></param>
        public int Iterate(IMove move, int depth, int alpha, int beta, bool player)
        {

            return 0;
        }

    }//end AlphaBetaMinimax

}//end namespace Algorithms
