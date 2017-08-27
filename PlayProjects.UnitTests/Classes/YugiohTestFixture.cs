using CardGamesAPI;
using CardGamesAPI.Yugioh;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static CardGamesAPI.Yugioh.YugiohCardFactory;

namespace PlayProjects.UnitTests
{
    public static class YugiohTestFixture
    {
        public static IEnumerable<IYugiohCard> CreateDeck(int numberOfCards = 40)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                //todo: create a random card type every time.
                yield return CreateBasicYugiohCard<MonsterCard>();
            }

        }

        public static YugiohMove CreateStandardGame(YugiohPlayer player1, YugiohPlayer player2)
        {
            YugiohMove move = new YugiohMove(player1, player2);

            (move.TurnPlayer as YugiohPlayer).Deck = CreateDeck().ToList();
            (move.Opponent as YugiohPlayer).Deck = CreateDeck().ToList();

            //move.DrawPhase();

            return move;
        }

        public static YugiohMove AddChildren(this YugiohMove move, int maxChildren, int depth)
        {
            Debug.WriteLine($"Depth: {depth} LifePoints: {move.GetScore(move.IsTurnPlayer)} IsTurnPlayer {move.IsTurnPlayer}");

            if (depth == 0)
            {
                return null;
            }

            move.Children.AddRange(CreateScoredMoves(maxChildren, move.IsTurnPlayer));

            foreach (YugiohMove childMove in move.Children)
            {
                childMove.AddChildren(maxChildren, depth - 1);
            }

            return move;

        }

        public static IEnumerable<string> GetTributeRules<T>()
        {
            string type = typeof(T).Name;
            return new List<string> { $"{type}.Level > 5", };
        }

        private static IEnumerable<IMove> CreateScoredMoves(int numberOfChildren, bool isCurrentPlayer)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                yield return new YugiohMove(isCurrentPlayer, new Random(DateTime.Now.Millisecond).Next(1, 80) * 50);
            }
        }

    }
}
