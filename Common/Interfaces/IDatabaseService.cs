using System.Collections.Generic;
namespace Common.Interfaces
{
    public interface IDatabaseService
    {
        //contract for item Database CRUD       
        //void SetConnectionString(string connectionString);
        //T Get<T>(T item); //get matching item from DB (for comparison purposes)
        //Guid GetUniqueId<T>(T item, string ColumName);
        bool Exists<T>(T item); //check that this item exists
        bool Insert<T>(T item);
        bool InsertCollection<T>(IEnumerable<T> items);
        bool Delete<T>(T item);
        bool DeleteCollection<T>(IEnumerable<T> items);
        bool Update<T>(T item);
        bool UpdateCollection<T>(IEnumerable<T> items);
        //List<T> Search<T>([Search params]); //Search by a set of parameters to check and their values, returns a list of T that match params
    }
}
