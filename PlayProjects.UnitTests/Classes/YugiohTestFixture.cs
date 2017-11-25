using CardGamesAPI;
using CardGamesAPI.Yugioh;
using CardGamesAPI.Yugioh.RulesEngine;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlayProjects.UnitTests
{
    public static class YugiohTestFixture
    {
        private static string ConnectionString = Properties.Settings.Default.YugiohConnectionString;

        //internal static IYugiohCard CreateCard<T>()
        //{
        //    return YugiohCardFactory.CreateRandomCard(typeof(T));//todo: update to pass the T, with constraint 'where : T class, new()'
        //}

        //internal static IYugiohCard CreateRandomCard()
        //{
        //    return YugiohCardFactory.CreateRandomCardType();
        //}

        //public static IEnumerable<IYugiohCard> CreateDeck(int numberOfCards = 40)
        //{
        //    for (int i = 0; i < numberOfCards; i++)
        //    {
        //        yield return YugiohCardFactory.CreateRandomCardType();
        //    }

        //}

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

        public static List<string> GetTributeRules()
        {
            return new List<string>
            {
                "Level > 5",
                "!Text.ToLower().Contains(\"cannot be normal summoned\")"
            };
        }

        public static List<YugiohRule> GetRuleBook()
        {
            var dt = RulesDAL.GetRuleBook(ConnectionString);
            return dt.ToList<YugiohRule>();
        }

        private static IEnumerable<IMove> CreateScoredMoves(int numberOfChildren, bool isCurrentPlayer)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                yield return new YugiohMove(isCurrentPlayer, new Random(DateTime.Now.Millisecond).Next(1, 80) * 50);
            }
        }

        internal static List<IYugiohCard> GetTop100Cards()
        {
            throw new NotImplementedException();
            //var dt = CardsDAL.GetTopCardsFromDb(ConnectionString, 100);
            //return dt.ToList<IYugiohCard>();

        }

    }

}
