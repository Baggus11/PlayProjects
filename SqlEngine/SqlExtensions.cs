using Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace DataAccess
{
    public static class SQLExtensions
    {
        private static readonly Logger NLogger = LogManager.GetCurrentClassLogger();

        public static R GetSingleRow<R>(this DbDataReader reader, Func<DbDataReader, R> selector)
        {
            try
            {
                R result = default(R);

                if (reader.Read())
                    result = selector(reader);

                if (reader.Read())
                    throw new DataException("multiple rows returned from query");

                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");

                throw;
            }
        }

        public static void AddCommandParams<T>(this SqlCommand command, IEnumerable<SqlParameter> sqlParams, T item)
        {
            try
            {
                foreach (var parameter in sqlParams)
                {
                    parameter.Value = item?.GetPropertyValue(parameter.ParameterName);
                    if (parameter.Value != null)
                        command.Parameters.Add(parameter);
                }
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Debug.WriteLine(errMsg);
            }
        }

        public static bool IsValidConnectionString(this string connectionString)
        {
            var csb = new SqlConnectionStringBuilder(connectionString);

            return (csb.DataSource == null || csb.InitialCatalog == null)
                ? throw new Exception($"Connection string: '{connectionString} invalid")
                : true;

        }

        public static bool CanOpen(this string connectionString) =>
            new SqlConnection(connectionString).CanOpen();

        public static bool CanOpen(this SqlConnection connection)
        {
            try
            {
                if (connection == null)
                    return false;

                connection.Open();
                var canOpen = connection.State == ConnectionState.Open;

                connection.Close();
                return canOpen;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                return false;
            }
        }

    }
}
