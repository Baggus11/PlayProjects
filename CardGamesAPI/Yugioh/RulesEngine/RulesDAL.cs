using Common;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CardGamesAPI.Yugioh.RulesEngine
{
    public static class DAL
    {
        public static List<IRule> GetAssignedRulesFor<T>()
        {
            return null;
        }

        public static bool InsertRuleFor<T>(IRule rule, T instance)
        {
            return true;
        }

        public static bool UpdateRuleFor<T>(IRule oldRule, IRule updatedRule)
        {
            return true;
        }

        private static bool RuleMatchesSchema<T>(IRule rule, SqlConnection connection)
        {
            //Check that type T matches the Rule Type in the DB.
            return true;
        }

    }

}
