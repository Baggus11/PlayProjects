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
        Func<INormalMonster, bool> advance_summon_condition = monster => monster.Level >= 5;
        private const string _opponentName = "Pie Five";
        private const string _playerName = "Dusty Tornado";
        string _monster_advanceSummon_Exception = "!MonsterCard.Text.Contains(\"cannot be Normal Summoned\")";

        List<YugiohRule> RuleBook = new List<YugiohRule>();

        [TestInitialize]
        public void Init()
        {
            RuleBook = YugiohTestFixture.GetRuleBook();
        }

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
            rootMove.Children.Select(child => new { Name = child.TurnPlayer.Name, Score = child.TurnPlayer.Score }).Dump("turn player");
            rootMove.Children.Select(child => new { Name = child.Opponent.Name, Score = child.Opponent.Score }).Dump("opponent");
        }

        [TestMethod]
        public void Can_Create_Random_Move_Tree()
        {
            var opponent = new YugiohPlayer(_opponentName);
            var root = new YugiohMove(opponent, 8000);
            root.AddChildren(2, 2);
            throw new NotImplementedException();

        }

        //[TestMethod]
        //[TestCategory("Card Conditions")]
        //public void Can_Check_Monster_Level()
        //{
        //    IMonsterCard kuriboh = new EffectMonster("Kuriboh", 2, 300, 200);
        //    IMonsterCard dark_magician = new NormalMonster("Dark Magician", 7, 300, 200);
        //    IMonsterCard highlevel_notribute_monster = new NormalMonster("Dark Magician", 7, 300, 200);
        //    highlevel_notribute_monster.Text = "This monster cannot be Normal Summoned.";

        //    //Assign
        //    var list = YugiohTestFixture.CreateDeck().OfType<IMonsterCard>().Where(advance_summon_condition);
        //    var advance_summon_rules = YugiohTestFixture.GetTributeRules();

        //    //Act
        //    var results = ExpressionBuilder.CheckCondition(list, advance_summon_rules.First());

        //    highlevel_notribute_monster.Dump("high level monster", ignoreNulls: true);

        //    //Assert
        //    Assert.IsFalse(ExpressionBuilder.CheckCondition(kuriboh, advance_summon_rules.First()));
        //    Assert.IsTrue(ExpressionBuilder.CheckCondition(dark_magician, advance_summon_rules.First()));
        //    Assert.IsFalse(ExpressionBuilder.CheckCondition(highlevel_notribute_monster, _monster_advanceSummon_Exception));

        //    Assert.IsNotNull(results);
        //    Assert.IsTrue(results.All(advance_summon_condition));
        //    results.Select(card => new
        //    {
        //        Name = card.CardName,
        //        Attack = card.Attack,
        //        Defense = card.Defense,
        //        Level = card.Level
        //    }).Dump("results", ignoreNulls: true);

        //}

        //[TestMethod]
        //public void Can_Advance_Summon_after_IRule_Check()
        //{
        //    //Assign
        //    YugiohPlayer player = new YugiohPlayer();
        //    IEffectMonster kuriboh = new EffectMonster("Kuriboh", 2, 300, 200);
        //    IMonsterCard dark_magician_girl = new EffectMonster("Dark Magician Girl", 6, 2000, 1600);
        //    IMonsterCard dark_armed_dragon = new EffectMonster("Dark Armed Dragon", 7, 2800, 2000);
        //    dark_armed_dragon.Text = "This monster cannot be Normal Summoned.  This monster can only be Summoned by...";
        //    var rules = YugiohTestFixture.GetTributeRules();

        //    //Summon Kuriboh:
        //    kuriboh.Position = YugiohCardPosition.SetFaceDown;
        //    player.MonsterZone.Add(kuriboh);

        //    //Act
        //    if (rules.All(rule => ExpressionBuilder.CheckCondition(dark_magician_girl, rule)))
        //    {
        //        player.MonsterZone.Remove(kuriboh);
        //        player.MonsterZone.Add(dark_magician_girl);
        //    }

        //    //Assert
        //    Assert.IsFalse(rules.All(rule => ExpressionBuilder.CheckCondition(dark_armed_dragon, rule)));
        //    Assert.IsTrue(player.MonsterZone.Count > 0);
        //    player.MonsterZone.Dump("monsters", ignoreNulls: true);
        //}

        [TestMethod]
        [TestCategory("Card Conditions")]
        public void Can_Check_Multiple_Basic_Rules()
        {
            //Assign
            var monsters = YugiohTestFixture.CreateDeck().OfType<INormalMonster>();
            var low_level_monster = monsters.First(m => m.Level < 5).Dump("low level monster");
            var high_level_monster = monsters.First(m => m.Level >= 5).Dump("high level monster");
            var rules = YugiohTestFixture.GetTributeRules();

            //Act
            var result1 = rules.All(r => ExpressionBuilder.CheckCondition(low_level_monster, r));
            var result2 = rules.All(r => ExpressionBuilder.CheckCondition(high_level_monster, r));

            //Assert
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);

        }

        [TestMethod]
        [TestCategory("Rule Book")]
        public void Can_Check_DB_Rule_Book()
        {
            var monsters = YugiohTestFixture.CreateDeck().OfType<IMonsterCard>();
            var allMonsters = monsters.All(RuleBook.Select(r => r.PreConditions));
            allMonsters.Dump("should always be null");

            var anyMonster = monsters.Any(RuleBook.Select(r => r.PreConditions));
            anyMonster.Take(5).Dump("Any monsters that match any rules in rulebook");

            //Assert
            Assert.IsTrue(allMonsters.Count() == 0);
            Assert.IsNotNull(anyMonster.Count() > 0 || anyMonster.Count() == monsters.Count());

        }

        [TestMethod]
        [TestCategory("Rule Book")]
        public void Check_Card_DB_Against_Rule_Book()
        {
            var monsters = YugiohTestFixture.GetTop100Cards();

            var allMonsters = monsters.All(RuleBook.Select(r => r.PreConditions));
            allMonsters.Dump("should always be null");

            var anyMonster = monsters.Any(RuleBook.Select(r => r.PreConditions));
            anyMonster.Take(5).Dump("Any should have 1+");

            //Assert
            Assert.IsTrue(allMonsters.Count() == 0);
            Assert.IsNotNull(anyMonster.Count() > 0 || anyMonster.Count() == monsters.Count());

        }
        //[TestMethod]
        //[TestCategory("Misc")]
        //public void Switch_On_Type_Examlpe<T>(T @object)
        //{

        //    var @switch = new Dictionary<Type, Action>()
        //    {
        //        {typeof(bool), ()=> {/* do bool things*/ } },
        //        {typeof(double), ()=> { /*do double stuff*/ } },
        //        {typeof(int), ()=> {  } },
        //        {typeof(Person), ()=> { /*person stuff*/ } },
        //        {typeof(IMonsterCard), ()=> {  } },
        //    };

        //    @switch[typeof(T)]();

        //}

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
