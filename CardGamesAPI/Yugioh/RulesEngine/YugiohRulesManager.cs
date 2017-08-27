using CardGamesAPI.Yugioh.RulesEngine;
using Common;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public class YugiohRulesManager : ExpressionBuilderBase
    {

        public YugiohRulesManager()
        {
        }

        public IEnumerable<IRule> GetRules<T>() => DAL.GetAssignedRulesFor<T>();
    }
}
