using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Common.Interfaces
{
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        private SqlConnectionStringBuilder ConnectionBuilder { get; set; }
        public DatabaseServiceBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
        public bool Delete<T>(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }
        public bool Delete<T>(T item)
        {
            throw new NotImplementedException();
        }
        public bool Insert<T>(IEnumerable<T> items)
        {
            string connectionstr = "";
            StringBuilder sql = new StringBuilder("INSERT INTO Database.dbo.Table");
            sql.Append("(Field1, Field2)");
            sql.Append("VALUES (@Field1, @Field2)"); //TODO: get generated SQL from object here
            int rowsChanged = 0;
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                try
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    //Debug.WriteLine(string.Format("{0}: Running Query:  {1}", MethodBase.GetCurrentMethod().Name.ToUpper(), sql.ToString()));
                    using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
                    {
                        command.Transaction = trans;
                        foreach (var item in items)
                        {
                            try
                            {
                                command.Parameters.Clear();
                                //command.Parameters.Add("@Field1", SqlDbType.VarChar, 50).Value = item.Field1;
                                //command.Parameters.Add("@Field2", SqlDbType.VarChar, 50).Value = item.Field2;
                                //TODO: finish your SQLparameter creator method
                                rowsChanged += command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                                trans.Rollback();
                                throw new Exception(errMsg);
                            }
                        }
                        if (rowsChanged > 0)
                        {
                            trans.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                }
            }
            throw new NotImplementedException();
        }
        public bool Insert<T>(T item)
        {
            throw new NotImplementedException();
        }
        public T Search<T>(T entityIdentifier)
        {
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
    }
}
