using Common.Extensions;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Common
{
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        protected string TableName { get; set; }
        protected string ConnectionString { get; set; }
        public DatabaseServiceBase(string connectionString, string tableName)
        {
            TableName = tableName;
            if (connectionString.CanOpen())
                ConnectionString = connectionString;
            else
                Debug.WriteLine($"Could not connect with (initial) connection string: {connectionString}");
        }
        public bool Insert<T>(IEnumerable<T> items)
        {
            if (items == null) return false;
            throw new NotImplementedException();
        }
        public bool Insert<T>(T item)
        {
            bool result = false;
            if (item == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
            ConnectionString.CanOpen();
            List<SqlParameter> sqlParams = ConnectionString.GetSqlParams(TableName);
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
                            command.CommandText = sqlParams.Where(p => p.Value != null).GetInsertQuery(TableName);
                            //Debug.WriteLine(string.Format("{0}: Running Query: {1}", MethodBase.GetCurrentMethod().Name.ToUpper(), command.CommandText));
                            rowsChanged += command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
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
        public bool Delete<T>(IEnumerable<T> items)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString)) throw new Exception($"bad connection string: {ConnectionString}");
            if (items == null) throw new NullReferenceException();
            throw new NotImplementedException();
        }
        public bool Delete<T>(T item)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString)) throw new Exception($"bad connection string: {ConnectionString}");
            if (item == null) throw new NullReferenceException();
            throw new NotImplementedException();
        }
        public bool Update<T>(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }
        public bool Update<T>(T item)
        {
            throw new NotImplementedException();
        }
        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public bool Exists<T>(T item)
        {

            throw new NotImplementedException();
        }

        public Guid GetUniqueId<T>(T item, string name = "")
        {
            //Get first Guid from db.table that matches the given name, or Single, else if more than one get first

            throw new NotImplementedException();
        }
    }
}
