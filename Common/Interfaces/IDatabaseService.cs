using System.Collections.Generic;

namespace Common
{
    //contract for item Database CRUD
    public interface IDatabaseService
    {
        bool Exists<T>(T item); //check that this item exists
        bool Insert<T>(T item);
        bool InsertCollection<T>(IEnumerable<T> items);
        bool Delete<T>(T item);
        bool DeleteCollection<T>(IEnumerable<T> items);
        bool Update<T>(T item);
        bool UpdateCollection<T>(IEnumerable<T> items);
    }
}
