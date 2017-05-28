using System.Collections.Generic;
using System.Data.SqlClient;
namespace Common.Interfaces
{
    public interface IDatabaseService
    {
        //contract for item Database CRUD
        string ConnectionString { get; set; }
        void SetConnectionString(string connectionString);
        T Search<T>(T entityIdentifier);
        bool Insert<T>(T item);
        bool Insert<T>(IEnumerable<T> items);
        bool Delete<T>(T item);
        bool Delete<T>(IEnumerable<T> items);
        bool Update<T>(T item);
        bool Update<T>(IEnumerable<T> items);
    }
}
