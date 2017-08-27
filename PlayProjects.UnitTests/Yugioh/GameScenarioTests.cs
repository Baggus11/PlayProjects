using CardGamesAPI;
using CardGamesAPI.Yugioh;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlayProjects.UnitTests.Yugioh
{
    [TestClass]
    public class GameScenarioTests
    {
        Func<MonsterCard, bool> advance_summon_condition = monster => monster.Level >= 5;
        private const string _opponentName = "Pie Five";
        private const string _playerName = "Dusty Tornado";
        //private IWinCondition _winCondition;

        [TestMethod]
        public void Can_Engine_Run_A_Simple_Minimax()
        {
            //Assemble            
            Random rng = new Random(DateTime.Now.Millisecond);
            var player = new YugiohPlayer(_playerName);
            var opponent = new YugiohPlayer(_opponentName);
            IMove rootMove = YugiohTestFixture.CreateStandardGame(player, opponent);
            int playerDecision = 0;

            //Act
            try
            {
                rootMove.Children.Add(new YugiohMove(opponent, 8500));
                rootMove.Children.Add(new YugiohMove(opponent, 7200));
                AlphaBetaMinimax minimax = new AlphaBetaMinimax();
                playerDecision = minimax.Iterate(rootMove, 5, -99999, 99999, true);
                //int opponentDecision = rootMove.Iterate(5, -99999, 99999, true);
                Debug.WriteLine(playerDecision);



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            //Assert
            //Debug.WriteLine(opponentDecision);

            rootMove.Children.Select(child => new { Name = child.TurnPlayer.Name, Score = child.TurnPlayer.Score }).Dump("turn player");
            rootMove.Children.Select(child => new { Name = child.Opponent.Name, Score = child.Opponent.Score }).Dump("opponent");

            //Assert.IsTrue(playerDecision == goal);
        }

        [TestMethod]
        public void Can_Create_Random_Move_Tree()
        {
            var opponent = new YugiohPlayer(_opponentName);
            var root = new YugiohMove(opponent, 8000);
            root.AddChildren(2, 2);


        }

        [TestMethod]
        public void Can_Engine_Pick_Strongest_Monster_First()
        {

        }

        [TestMethod]
        public void Can_Advance_Summon()
        {
            MonsterCard kuriboh = new MonsterCard("Kuriboh", 0, 0, 0, 2, 300, 200);
            MonsterCard dark_magician = new MonsterCard("Dark Magician", 0, 0, 0, 7, 300, 200);
            MonsterCard highlevel_notribute_monster = new MonsterCard("Dark Magician", 0, 0, 0, 7, 300, 200);
            highlevel_notribute_monster.Text = "This monster cannot be Normal Summoned.";
            //Assign
            var list = YugiohTestFixture.CreateDeck().OfType<MonsterCard>().Where(advance_summon_condition);
            var advance_summon_rules = YugiohTestFixture.GetTributeRules<MonsterCard>();
            YugiohRulesManager manager = new YugiohRulesManager();

            //Act
            var results = manager.CheckRule(list, advance_summon_rules.First());

            string cannot_normal_summon = "!MonsterCard.Text.Contains(\"cannot be Normal Summoned\")";
            highlevel_notribute_monster.Dump("high level monster");

            //Assert
            Assert.IsFalse(manager.CheckRule(kuriboh, advance_summon_rules.First()));
            Assert.IsTrue(manager.CheckRule(dark_magician, advance_summon_rules.First()));
            Assert.IsFalse(manager.CheckRule(highlevel_notribute_monster, cannot_normal_summon));

            Assert.IsNotNull(results);
            Assert.IsTrue(results.All(advance_summon_condition));
            results.Select(card => new
            {
                Name = card.CardName,
                Attack = card.Attack,
                Defense = card.Defense,
                Level = card.Level
            }).Dump("results");

        }

        [TestMethod]
        public void Can_Advance_Summon_after_IRule_Check()
        {
            //Assign


        }
    }

    #region WinCondition Implementation (Attempt)

    public interface IWinCondition
    {
        //Goal heuristic that takes an evaluator
        bool IsSatisfied { get; }
        IEnumerable<IWinCondition> GetNextWinConditions(IMove nextMove);
    }

    public class YugiohWinEvaluator
    {
        //Check Exodia

        //Check Deck-out

        //Check FINAL

    }

    public class YugiohWinCondition : IWinCondition
    {
        public bool IsSatisfied => throw new NotImplementedException();

        public IEnumerable<IWinCondition> GetNextWinConditions(IMove nextMove)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

}
