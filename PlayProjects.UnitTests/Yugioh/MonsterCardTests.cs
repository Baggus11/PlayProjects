﻿using CardGamesAPI.Yugioh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Yugioh.Tests
{
    [TestClass()]
    public class MonsterCardTests
    {
        private List<IYugiohCard> Hand = new List<IYugiohCard>();
        private string conditionStr;
        Type objType;

        YugiohRulesManager rule_manager = new YugiohRulesManager();

        //[TestInitialize]
        //public void Init()
        //{
        //    objType = typeof(IMonsterCard);
        //    conditionStr = $@"{objType.Name}.Attack > 2300 AND {objType.Name}.MonsterAttribute = {YugiohMonsterAttribute.Dark.ToExpressionEnumCondition()}"
        //       + $@" AND {objType.Name}.MonsterType = {YugiohMonsterType.Fairy.ToExpressionEnumCondition()}";

        //    Console.WriteLine(conditionStr);

        //    Hand = new List<IYugiohCard>
        //    {
        //        new NormalMonster ( "Dark Magician", 7, 2500, 2100),
        //        new NormalMonster("Blue-Eyes White Dragon", 8, 3000, 2500),
        //        new EffectMonster("Dark Magician Girl", 5, 2300, 2000),
        //    };
        //}

        //[TestMethod]
        //[TestCategory("Misc")]
        //public void Can_Card_Be_Copied()
        //{
        //    var card1 = YugiohTestFixture.CreateCard<IMonsterCard>();
        //    IMonsterCard card2 = default(IMonsterCard);
        //    ObjectExtensions.Map(ref card1, ref card2);

        //    card1.Dump("card1");
        //    card2.Dump("card2");

        //    Assert.AreEqual(card1, card2);

        //}

        [TestMethod()]
        public void Build_And_Run_Expression_Test()
        {
            try
            {
                var paramExpr = Expression.Parameter(objType, objType.Name);
                LambdaExpression lambdaExpr = System.Linq.Dynamic.DynamicExpression.ParseLambda(new ParameterExpression[] { paramExpr }, null, conditionStr);
                lambdaExpr.ToString().Dump("lambda");
                Hand.GetItemsWhere(lambdaExpr).Dump("Found cards");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        public void IEnumerable_Shuffle_Extension_Test()
        {
            var list = Enumerable.Range(1, 40).ToList();
            var result = list.Shuffle();
            result.Dump("shuffled");
        }
        //[TestMethod]
        //public void RunEffectFromConditionTest()
        //{
        //    string txt = "Destroy(Card.BaseType == \"Monster\")";
        //    string rgx = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";
        //    //Find Matches:
        //    var match = Regex.Match(txt, rgx);
        //    string innerArgs = match.Groups[1].Value;
        //    MatchCollection matches = Regex.Matches(innerArgs, rgx);
        //    string conditionStr = matches[0].Value;
        //    string callMethod = txt.Replace(innerArgs, "");
        //    callMethod.Dump(nameof(callMethod));
        //    conditionStr.Dump(nameof(conditionStr));
        //    //Parse the correct lambda expr to run on a Card collection:
        //    if (!string.IsNullOrWhiteSpace(conditionStr))
        //    {
        //        Type type = typeof(Card);
        //        var paramExpr = Expression.Parameter(type, type.Name);
        //        LambdaExpression lambdaExpr = System.Linq.Dynamic.DynamicExpression.ParseLambda(new ParameterExpression[] { paramExpr }, null, conditionStr);
        //        lambdaExpr.ToString().Dump(nameof(lambdaExpr));
        //        //Call Method on the Cards that fit the condition!:D
        //        var cardsAffected = Field.GetElementsWhere(lambdaExpr).ToList();
        //        //Doing this step because I have not created an 'All' Enum condition just yet!:
        //        foreach (var item in cardsAffected)
        //        {
        //            try
        //            {
        //                //Type.GetType("CardActions")
        //                //    .GetMethod(callMethod, BindingFlags.Static | BindingFlags.Public)
        //                //    .Invoke(null, new object[] { item });
        //                ExecuteMethod(typeof(CardActions).Namespace, "CardActions", callMethod, new object[] { item });
        //            }
        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine(ex.Message);
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// Execute a method on an assembly's static class
        ///// </summary>
        ///// <param name="assemblyNamespace">The namespace of the assembly you would like to invoke</param>
        ///// <param name="className">The name of the class in the assembly you would like to invoke</param>
        ///// <param name="methodToExecute">The name of the method in the class to invoke</param>
        ///// <param name="parameters">array of objects to be passed into the method we're invoking</param>
        ///// <returns>object of whatever the method invoked returns</returns>
        //public static object ExecuteMethod(string assemblyNamespace, string className, string methodToExecute,
        //    params object[] parameters)
        //{
        //    try
        //    {
        //        Assembly assembly = Assembly.Load(assemblyNamespace);
        //        Type classType = assembly.GetType(string.Format("{0}.{1}", assemblyNamespace, className));
        //        MethodInfo methodInfo = classType.GetMethod(methodToExecute);
        //        return methodInfo.Invoke(null, parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }
}