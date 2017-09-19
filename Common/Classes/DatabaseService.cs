using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// ToDO: Add functionality that converts LINQ where, orderby, and having expressions their SQL counterparts
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        //TODO: make this absract base that can create, update, insert and select on tables, based on <T>

        protected string TableName { get; set; }
        protected string ConnectionString { get; set; }

        public DatabaseService(string connectionString, string tableName)
        {
            TableName = tableName;
            if (connectionString.CanOpen())
                ConnectionString = connectionString;
            else
                Debug.WriteLine($"Could not connect with (initial) connection string: {connectionString}");
        }

        public virtual bool Insert<T>(T item)
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

        public bool InsertCollection<T>(IEnumerable<T> items)
        {
            bool result = false;
            if (items == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
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
                        foreach (var item in items)
                        {
                            try
                            {
                                command.Parameters.Clear();
                                command.AddCommandParams(sqlParams, item);
                                command.Connection = connection;
                                command.CommandText = sqlParams.Where(p => p.Value != null).GetInsertQuery(TableName);
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

        public bool Delete<T>(T item)
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
                            command.CommandText = sqlParams.Where(p => p.Value != null).GetDeleteQuery(TableName);
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

        public bool DeleteCollection<T>(IEnumerable<T> items)
        {
            bool result = false;
            if (items == null) throw new NullReferenceException(MethodBase.GetCurrentMethod().Name);
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
                                command.CommandText = sqlParams.Where(p => p.Value != null).GetDeleteQuery(TableName);
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

        public bool Update<T>(T item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCollection<T>(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public bool Exists<T>(T item)
        {
            throw new NotImplementedException();
        }

        //public Guid GetUniqueId<T>(T item, string name = "")
        //{
        //    //Get first Guid from db.table that matches the given name, or Single, else if more than one get first
        //    throw new NotImplementedException();
        //}

    }
}
