using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Common.Interfaces
{
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        private SqlConnectionStringBuilder ConnectionBuilder { get; set; }
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
