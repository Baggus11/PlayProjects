using Common;
using System;
namespace CardGamesAPI.Yugioh
{
    public class CardDatabaseService : DatabaseServiceBase
    {
        public CardDatabaseService(string connectionString)
            : base(connectionString, "Cards")
        {
        }
        public override bool Insert<T>(T item)
        {
            //Todo: Create an implementation that relies on Expressions/LINQs that being translated directly to SQL
            //...
            //List<SqlParameter> sqlParams = ConnectionString.GetSqlParams(TableName);
            //...
            //foreach(var parameter in sqlParams)
            //  parameter.Value = item?.GetPropertyValue(parameter.ParameterName);
            //...
            //command.CommandText = sqlParams.Where(p => p.Value != null).GetInsertQuery(TableName);
            throw new NotImplementedException();
        }
    }
}
