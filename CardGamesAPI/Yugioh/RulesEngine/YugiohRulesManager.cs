using CardGamesAPI.Yugioh.RulesEngine;
using Common;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public class YugiohRulesManager
    {
        public YugiohRulesManager()
        {
        }

        public IEnumerable<IRule> GetRules<T>() => RulesDAL.GetAssignedRulesFor<T>();
    }
}
