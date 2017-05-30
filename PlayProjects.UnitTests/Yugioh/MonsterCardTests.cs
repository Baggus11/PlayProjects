using CardGames;
using CardGamesAPI.Yugioh;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Common.Yugioh.Tests
{
    [TestClass()]
    public class MonsterCardTests
    {
        public static List<IYugiohCard> Hand = new List<IYugiohCard>();
        public MonsterCardTests()
        {
            Hand = new List<IYugiohCard>
            {
                new MonsterCard ( "Dark Magician", YugiohMonsterAttribute.Dark, YugiohMonsterType.Spellcaster, YugiohMonsterBaseType.Normal, 2500, 2100),
                new MonsterCard("Blue-Eyes White Dragon", YugiohMonsterAttribute.Light, YugiohMonsterType.Dragon, YugiohMonsterBaseType.Normal, attack: 3000, defense: 2500),
                new MonsterCard("Dark Magician Girl", YugiohMonsterAttribute.Dark, YugiohMonsterType.Spellcaster, YugiohMonsterBaseType.Effect, 2300, 2000),
                //new SpellCard { CardName = "Dark Hole" },
                //new TrapCard { CardName = "Celtic Guardian" },
            };
        }
        [TestMethod()]
        public void MonsterCardTest()
        {
            MonsterCard card = new MonsterCard("BEWD", YugiohMonsterAttribute.Light, YugiohMonsterType.Dragon, YugiohMonsterBaseType.Normal);
            card.Dump();
            Assert.IsNotNull(card);
        }

        [TestMethod()]
        public void BuildAndRunExpressionTest()
        {
            try
            {
                //NOTE: I had to use 1.4 instead of 1.6 for System.Linq.Dynamic because 2015 has weird bug...
                Type type = typeof(MonsterCard);
                string conditionStr = $@"{type.Name}.Attack > 2300";
                //string conditionStr = $@"{type.Name}.Attack > 2300 AND {type.Name}.MonsterAttribute = YugiohMonsterAttribute.Dark"; //find way to check enums, or make this private string (setter uses enums)
                var paramExpr = Expression.Parameter(type, type.Name);
                LambdaExpression lambdaExpr = System.Linq.Dynamic.DynamicExpression.ParseLambda(new ParameterExpression[] { paramExpr }, null, conditionStr);
                lambdaExpr.ToString().Dump("lambda");
                Hand.GetElementsWhere(lambdaExpr).Dump("FOUND CARDS");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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