using CardGamesAPI.Yugioh;
using System;
using System.Collections.Generic;

namespace CardGamesAPI
{
    public abstract class MoveBase : IMove
    {
        public virtual IPlayer TurnPlayer { get; set; }
        public virtual IPlayer Opponent { get; set; }
        public List<IMove> Children { get; set; }
        public int TurnNumber { get; set; }

        public void Dispose()
        {
            if (Children?.Count > 0)
            {
                foreach (var item in Children)
                {
                    item.Dispose();
                }
            }

            TurnPlayer = null;
            Opponent = null;
        }

        public abstract int GetScoreOf(IPlayer player);

        public abstract int GetScore(bool turnPlayer);

        public abstract bool IsTerminal(bool turnPlayer);

        public abstract void GenerateMoves();
    }

    public interface IMove : IDisposable
    {
        IPlayer TurnPlayer { get; set; }
        IPlayer Opponent { get; }
        int TurnNumber { get; set; }
        List<IMove> Children { get; set; }

        bool IsTerminal(bool player);
        int GetScoreOf(IPlayer player);
        int GetScore(bool turnPlayer);

        void GenerateMoves();

    }

    public class YugiohMoveGenerator
    {
        private IMove Parent { get; set; }
        public List<Action> PossibleActions { get; set; }
        private int _MaxChildren;

        YugiohMoveGenerator(ref IMove parent, int maxChildren = 0)
        {
            Parent = parent;
            _MaxChildren = maxChildren;
        }

        //exactly what it says...
        private void GenerateBestMoveSequenceFromAvailableActions()
        {
            //Set the move
        }

        //Add actions on the move to be executed.
        //Several actions comprise a move, minimum: 1.
        private void AddActionSequence(IMove parent, params Action<IMove>[] actions)
        {
            //Add actions determined by
        }

        private void ExecuteActions(IMove parent)
        {
            //Execute stored actions
        }

        private static void ExecuteAction<T>(T item, Action<T> work)
        {
            work(item);
        }

    }

    public static class YugiohActions
    {
        public static Action<IMove> EndPhase = (move) =>
        {
        };

        public static Action<YugiohMove, int> TurnPlayerDraws = (move, num) =>
         {
             move.TurnPlayer.Draw(num);
         };

        //public void DrawPhase()
        //{
        //    TurnPlayerDraws(CurrentDrawSize);
        //}

        //public void EndPhase()
        //{
        //    TurnNumber++;
        //    TurnPlayer = Opponent;
        //}

        //public void TurnPlayerDraws(int numberOfCards = 1) => TurnPlayer.Draw(numberOfCards);

        //public void OpponentDraws(int numberOfCards = 1) => Opponent.Draw(numberOfCards);

    }


}
