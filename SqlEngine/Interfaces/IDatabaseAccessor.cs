using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface IDatabaseAccessor
    {
        //bool RowExists<T>(T item) where T : class, new();
        bool Insert<T>(T item)
            where T : class, new();
        bool InsertCollection<T>(IEnumerable<T> items)
            where T : class, new();
        bool Delete<T>(T item)
            where T : class, new();
        bool DeleteCollection<T>(IEnumerable<T> items)
            where T : class, new();
        bool Update<T>(T item)
            where T : class, new();
        bool UpdateCollection<T>(IEnumerable<T> items)
            where T : class, new();
        SqlParameter[] CreateSqlParamsFromClass<T>();
        SqlParameter CreateSqlParamFromPropertyValue<T>(T propertyValue, string propertyName);
    }

}
