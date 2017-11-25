using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccessLayers
{
    /// <summary>
    ///Absract base class which creates, updates, inserts and selects based on typeof(T)    
    /// </summary>
    public abstract class DataAccessLayerBase : IDataAccessLayer
    {
        protected string TableName { get; set; }
        protected string ConnectionString { get; set; }

        public DataAccessLayerBase(string connectionString, string tableName)
        {
            TableName = tableName;
            if (connectionString.CanOpen())
                ConnectionString = connectionString;
            else
                Debug.WriteLine($"Could not connect with (initial) connection string: {connectionString}");
        }

        public bool Insert<T>(T item) where T : class, new() =>
            Insert(item, ParameterCreationOption.FromClass);

        public virtual bool Insert<T>(T item, ParameterCreationOption option = ParameterCreationOption.FromClass)
             where T : class, new()
        {
            if (item == null)
                throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);

            bool result = false;

            List<SqlParameter> sqlParams = (option != ParameterCreationOption.FromClass)
                ? GetSqlParamsFromTable(ConnectionString, TableName).Where(p => p != null).ToList()
                : CreateSqlParamsFromClass<T>().Where(p => p != null).ToList();

            if (sqlParams?.Count == 0)
                throw new Exception($"No sql params found or not all could be generated for class '{typeof(T).Name}'!");
            sqlParams.Dump("sql params");
            int rowsChanged = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Transaction = trans;
                        try
                        {
                            command.Parameters.Clear();
                            foreach (var parameter in sqlParams)
                            {
                                parameter.Value = item?.GetPropertyValue(parameter?.ParameterName);
                                if (parameter.Value != null)
                                    command.Parameters.Add(parameter);
                            }
                            command.Connection = connection;
                            command.CommandText = CreateInsertQueryFromSqlParameters(sqlParams, TableName);
                            rowsChanged += command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: Query '{command.CommandText}' failed!\n {ex.ToString()}");
                            trans.Rollback();
                            connection.Close();
                            result = false;
                            throw;
                        }
                    }
                    if (rowsChanged > 0)
                    {
                        trans.Commit();
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                    result = false;
                    connection.Close();
                }
            }
            return result;
        }

        public virtual bool InsertCollection<T>(IEnumerable<T> items)
             where T : class, new()
        {
            bool result = false;
            if (items == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
            ConnectionString.CanOpen();
            List<SqlParameter> sqlParams = GetSqlParamsFromTable(ConnectionString, TableName);
            int rowsChanged = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Transaction = trans;
                        foreach (var item in items)
                        {
                            try
                            {
                                command.Parameters.Clear();
                                command.AddCommandParams(sqlParams, item);
                                command.Connection = connection;
                                command.CommandText = CreateInsertQueryFromSqlParameters(sqlParams.Where(p => p.Value != null), TableName);
                                rowsChanged += command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: Query '{command.CommandText}' failed!\n {ex.ToString()}");
                                trans.Rollback();
                                connection.Close();
                                result = false;
                                break;
                            }
                        }
                    }
                    if (rowsChanged > 0)
                    {
                        trans.Commit();
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                    result = false;
                    connection.Close();
                }
            }
            return result;
        }

        public virtual bool Delete<T>(T item)
             where T : class, new()
        {
            bool result = false;
            if (item == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
            ConnectionString.CanOpen();
            List<SqlParameter> sqlParams = GetSqlParamsFromTable(ConnectionString, TableName);
            int rowsChanged = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Transaction = trans;
                        try
                        {
                            command.Parameters.Clear();
                            foreach (var parameter in sqlParams)
                            {
                                parameter.Value = item?.GetPropertyValue(parameter.ParameterName);
                                if (parameter.Value != null)
                                    command.Parameters.Add(parameter);
                            }

                            command.Connection = connection;
                            command.CommandText = CreateDeleteQueryFromSqlParameters(sqlParams.Where(p => p.Value != null), TableName);
                            rowsChanged += command.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: Query '{command.CommandText}' failed!\n {ex.ToString()}");
                            trans.Rollback();
                            connection.Close();
                            result = false;

                        }
                    }
                    if (rowsChanged > 0)
                    {
                        trans.Commit();
                        result = true;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                    result = false;
                    connection.Close();
                }
            }
            return result;
        }

        public virtual bool DeleteCollection<T>(IEnumerable<T> items)
            where T : class, new()
        {
            bool result = false;
            if (items == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
            ConnectionString.CanOpen();
            List<SqlParameter> sqlParams = GetSqlParamsFromTable(ConnectionString, TableName);
            int rowsChanged = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Transaction = trans;
                        foreach (var item in items)
                        {
                            try
                            {
                                command.Parameters.Clear();
                                foreach (var parameter in sqlParams)
                                {
                                    parameter.Value = item?.GetPropertyValue(parameter.ParameterName);
                                    if (parameter.Value != null)
                                        command.Parameters.Add(parameter);
                                }

                                command.Connection = connection;
                                command.CommandText = CreateDeleteQueryFromSqlParameters(sqlParams.Where(p => p.Value != null), TableName);
                                rowsChanged += command.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: Query '{command.CommandText}' failed!\n {ex.ToString()}");
                                trans.Rollback();
                                connection.Close();
                                result = false;

                            }
                        }
                        if (rowsChanged > 0)
                        {
                            trans.Commit();
                            result = true;

                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                    result = false;
                    connection.Close();
                }
            }
            return result;
        }

        public virtual bool Update<T>(T item)
             where T : class, new() =>
                throw new NotImplementedException();

        public virtual bool UpdateCollection<T>(IEnumerable<T> items)
             where T : class, new() =>
                throw new NotImplementedException();

        public virtual SqlParameter[] CreateSqlParamsFromClass<T>()
        {
            try
            {
                return typeof(T).GetProperties().Select(p =>
                                CreateSqlParamFromType(p.PropertyType, (Attribute.IsDefined(p, typeof(DatabaseAliasAttribute)))
                                ? p.GetCustomAttributes(inherit: false).OfType<DatabaseAliasAttribute>().Single().Alias
                                : p.Name)).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                throw;
            }

        }

        public virtual SqlParameter CreateSqlParamFromPropertyValue<T>(T propertyValue, string propertyName)
        {
            try
            {
                var @switch = GetSqlParameterSwitcher(propertyName);

                SqlParameter parameter = (SqlParameter)@switch[typeof(T)].Value;
                parameter.Value = propertyValue;
                return parameter;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                throw;
            }
        }

        private static SqlParameter CreateSqlParamFromType(Type type, string propertyName)
        {
            try
            {
                var @switch = GetSqlParameterSwitcher(propertyName);
                return @switch[type];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                throw;
            }

        }

        private static List<SqlParameter> GetSqlParamsFromTable(string connectionString, string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new Exception("No table name provided!");

            if (!connectionString.IsValidConnectionString())
                return null;

            List<SqlDbType> sqlDbTypes = new List<SqlDbType>();
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = $"SET FMTONLY ON; select * from dbo.{tableName}; SET FMTONLY OFF";
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt = reader.GetSchemaTable();
                }
            }
            //
            /// Get the name of column, type of column and size and assign to tuples
            ////
            List<Tuple<string, SqlDbType, int>> sql_pre_param_list = new List<Tuple<string, SqlDbType, int>>();
            foreach (var row in dt.Rows.Cast<DataRow>())
            {
                SqlDbType type = (SqlDbType)(int.Parse(row["ProviderType"].ToString()));
                int size = int.Parse(row["ColumnSize"].ToString());
                string name = row["ColumnName"].ToString();
                //Debug.WriteLine($"type: {type.ToString()}; size: {size}");
                Tuple<string, SqlDbType, int> tuple = new Tuple<string, SqlDbType, int>(name, type, size);
                sql_pre_param_list.Add(tuple);
            }
            //
            /// Create full parameter list!
            ////
            List<SqlParameter> sql_params_list = new List<SqlParameter>();
            foreach (var item in sql_pre_param_list)
            {
                SqlParameter sp = new SqlParameter
                {
                    ParameterName = item.Item1,
                    SqlDbType = item.Item2,
                    Size = item.Item3
                };
                sql_params_list.Add(sp);
            }
            //sql_params_list.ForEach(x => { Debug.WriteLine($"{x.ParameterName}\t{x.SqlDbType.ToString()}\t{x.Size}"); });
            return sql_params_list;
        }

        private static Dictionary<Type, SqlParameter> GetSqlParameterSwitcher(string propertyName)
        {
            return new Dictionary<Type, SqlParameter>()
                {
                    { typeof(bool), new SqlParameter(propertyName, SqlDbType.Bit) },
                    { typeof(int), new SqlParameter(propertyName, SqlDbType.Int) },
                    { typeof(double), new SqlParameter(propertyName, SqlDbType.Float) },
                    { typeof(float), new SqlParameter(propertyName, SqlDbType.Float) },
                    { typeof(decimal), new SqlParameter(propertyName, SqlDbType.Decimal) },
                    { typeof(DateTime), new SqlParameter(propertyName, SqlDbType.DateTime) },
                    { typeof(string), new SqlParameter(propertyName, SqlDbType.VarChar) },
                };
        }

        private static string CreateInsertQueryFromSqlParameters(IEnumerable<SqlParameter> sqlparameters, string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return null;

            StringBuilder query = new StringBuilder($"INSERT INTO dbo.{tableName}\n");

            try
            {

                query.Append("(");

                foreach (var parameter in sqlparameters)
                    query.Append($"{parameter.ParameterName}, ");

                query.Length -= 2; //removes extra comma
                query.Append(")\nVALUES\n(");

                foreach (var parameter in sqlparameters)
                    query.Append($"@{parameter.ParameterName}, ");

                query.Length -= 2; //removes extra comma
                query.Append(")");

            }
            catch (Exception)
            {
                throw;

            }

            return query.ToString();

        }

        private static string CreateDeleteQueryFromSqlParameters(IEnumerable<SqlParameter> sqlparameters, string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return null;

            StringBuilder query = new StringBuilder($"DELETE FROM dbo.{tableName}\n");

            query.Append("\nWHERE\n");

            foreach (var parameter in sqlparameters)
            {
                GetEqualsCase(parameter, query, SqlQueryOperator.AND);
            }

            query.Length -= 4; //removes extra AND

            return query.ToString();

        }

        //TODO: Finish implementing the cases using a switchionary (dictionary used as a switch statement)!
        private static void GetEqualsCase(SqlParameter parameter, StringBuilder query, SqlQueryOperator op)
        {
            switch (parameter.SqlDbType)
            {
                case SqlDbType.BigInt:
                    break;
                case SqlDbType.Binary:
                    break;
                case SqlDbType.Bit:
                    break;
                case SqlDbType.Char:
                    break;
                case SqlDbType.DateTime:
                    break;
                case SqlDbType.Decimal:
                    break;
                case SqlDbType.Float:
                    break;
                case SqlDbType.Image:
                    break;
                case SqlDbType.Int:
                    query.Append($"{parameter.ParameterName} = {(int)parameter.Value} {op.ToString()} ");
                    break;
                case SqlDbType.Money:
                    break;
                case SqlDbType.NChar:
                    break;
                case SqlDbType.NText:
                    break;
                case SqlDbType.NVarChar:
                    break;
                case SqlDbType.Real:
                    break;
                case SqlDbType.UniqueIdentifier:
                    break;
                case SqlDbType.SmallDateTime:
                    break;
                case SqlDbType.SmallInt:
                    break;
                case SqlDbType.SmallMoney:
                    break;
                case SqlDbType.Text:
                    break;
                case SqlDbType.Timestamp:
                    break;
                case SqlDbType.TinyInt:
                    break;
                case SqlDbType.VarBinary:
                    break;
                case SqlDbType.VarChar:
                    query.Append($"{parameter.ParameterName} = '{parameter.Value.ToString()}' {op.ToString()} ");
                    break;
                case SqlDbType.Variant:
                    break;
                case SqlDbType.Xml:
                    break;
                case SqlDbType.Udt:
                    break;
                case SqlDbType.Structured:
                    break;
                case SqlDbType.Date:
                    break;
                case SqlDbType.Time:
                    break;
                case SqlDbType.DateTime2:
                    break;
                case SqlDbType.DateTimeOffset:
                    break;
                default:
                    break;
            }
        }

        private static DataTable FindDuplicateRows(string connectionString, string tablename, IEnumerable<string> excludedColumns = null, int max_appearances_allowed = 1)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(connectionString);
            string database = csb.InitialCatalog;
            string connStr = csb.ConnectionString;
            List<string> nonPKeyColumnNames = new List<string>();
            string sql_get_schema = $"SET FMTONLY ON; select * from [{database}].dbo.[{tablename}]; SET FMTONLY OFF";
            //
            /// GET SCHEMA and NON-UNIQUEIDENTIFIER COLUMN NAMES
            ////
            DataTable dtSchema = new DataTable($"{tablename} Schema");
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand(sql_get_schema, connection))
                {
                    try
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dtSchema = reader.GetSchemaTable();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                    }
                }
            }
            foreach (var row in dtSchema.Rows.Cast<DataRow>())
            {
                SqlDbType type = (SqlDbType)(int.Parse(row["ProviderType"].ToString()));
                if (type != SqlDbType.UniqueIdentifier)
                    nonPKeyColumnNames.Add(row["ColumnName"].ToString());
            }
            if (excludedColumns != null) nonPKeyColumnNames.RemoveAll(x => excludedColumns.Contains(x));
            //
            /// RETRIEVE DUPLICATES
            //// 
            DataTable dt = new DataTable($"{tablename} Duplicates");
            StringBuilder sql = new StringBuilder();
            try
            {
                List<string> correctedColumNames = new List<string>();
                foreach (var name in nonPKeyColumnNames)
                {
                    if (!name.EndsWith(","))
                        correctedColumNames.Add(name + ",");
                    else correctedColumNames.Add(name);
                }
                correctedColumNames[correctedColumNames.Count - 1] = correctedColumNames.Last().Replace(",", " ").Trim();
                sql.Append("SELECT ");
                //Add Columns Names:
                foreach (string columnName in correctedColumNames)
                    sql.Append(string.Format("{0} ", columnName));
                sql.Append(", count (*) as APPEARANCES ");
                sql.AppendLine(string.Format(" \nFROM {0}.dbo.{1}", database, tablename));
                sql.AppendLine(" Group By ");
                //Add Columns Names again:
                foreach (string columnName in correctedColumNames)
                    sql.Append(string.Format("{0} ", columnName));
                sql.AppendLine(" having count (*) > " + max_appearances_allowed);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            using (SqlDataAdapter da = new SqlDataAdapter(sql.ToString(), connStr))
            {
                try
                {
                    da.SelectCommand.CommandTimeout = 180;
                    da.Fill(dt);
                    if (dt.Rows.Count > max_appearances_allowed)
                    {
                        Debug.WriteLine(string.Format("--Number of Duplicate rows (including original) in table {0}.dbo.{1}: {2}", database, tablename, dt.Rows.Count));
                        Debug.WriteLine(sql.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                }
            }
            return dt;
        }

    }
    /// <summary>
    /// Abstract base class with provides any POCO class a means of connecting and performing sql operations instantly.
    /// Some SQL will be built by dynasql.
    /// Raw SQL may be used by the child class.
    /// </summary>
    /// <typeparam name="TChild"></typeparam>
    public abstract class EntityBase<TChild> //Note: I want the type of the child that derives from this abstract base to drive the generation of sqlparams AND what gets searched, inserted, deleted, merged, joined, etc.
    {
        public bool MatchesSchema { get; } //runs CheckSchema();
                                           //Query SqlQuery //Fully formed query by DynaSql or whatever you find.

        public SqlConnectionStringBuilder ConnectionStringBuilder { get => _connectionStringBuilder; set => _connectionStringBuilder = value; }

        protected SqlParameter[] TableParameters { get; } //parameters derived from sql table
        protected SqlParameter[] DerivedParameters { get; } //params generated from derived class

        //TODO: create a method that allows switching between parameter sets. 
        //TODO: create a method that creates both sets of parameters.

        private SqlConnectionStringBuilder _connectionStringBuilder; //Facilitates building and checking connection strings

        protected internal bool CheckSchema<TChild>()
        {
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        } //Checks TChild to see if the child class in any way matches the database specified.

        public EntityBase(string connectionString) //verifies connectionstring is in proper format, else throws exception.  Iff solid, 
        { }

        internal abstract bool GetSqlParams(); //Already implemeted by DAL
        internal abstract void CreateSqlParams(); //Create and store both sets of SqlParams.  Throw if either set is null and specify.

        public abstract void SwapParameterSet(ParameterCreationOption option); //Warn if one set or both are null.

        public virtual bool RowExists<T>(T item) //check that an item exists in the table
            where T : class, new() =>
                throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);


        //void RunQuery(); //Run the query built up in this object so far.  (this will be a modified version of CWAccess.SqlResult)
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseAliasAttribute : Attribute
    {
        public string Alias { get; set; }
        public DatabaseAliasAttribute(string alias)
        {
            Alias = alias;
        }

    }

    public static class SQLExtensions
    {
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
