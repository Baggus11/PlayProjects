using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CardGamesAPI.Yugioh.RulesEngine
{
    public static class RulesDAL
    {
        public static DataTable GetRuleBook(string connectionString)
        {
            string sql = "select * from dbo.Rules";

            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand sqlconnection = new SqlCommand(sql, new SqlConnection(connectionString)))
                using (SqlDataAdapter da = new SqlDataAdapter(sqlconnection))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                dt = null;
                throw;
            }

        }

        public static List<Common.Rule> GetAssignedRulesFor<T>()
        {
            throw new NotImplementedException();

        }

        public static bool InsertRuleFor<T>(Common.Rule rule, T instance)
        {
            throw new NotImplementedException();

        }

        public static bool UpdateRuleFor<T>(Common.Rule oldRule, Common.Rule updatedRule)
        {
            throw new NotImplementedException();

        }

        private static bool RuleMatchesSchema<T>(Common.Rule rule, SqlConnection connection)
        {
            //Check that type T matches the Rule Type in the DB.
            throw new NotImplementedException();
        }

    }

    public static class CardsDAL
    {

        public static DataTable GetTopCardsFromDb(string connectionString, int count)
        {
            if (count < 0) return null;

            string sql = $"select top {count} * from dbo.cards";

            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand sqlconnection = new SqlCommand(sql, new SqlConnection(connectionString)))
                using (SqlDataAdapter da = new SqlDataAdapter(sqlconnection))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                dt = null;
                throw;
            }
        }

    }

}
