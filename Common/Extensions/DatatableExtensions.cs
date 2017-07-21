using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Datatable to ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this DataTable table) where T : class, new()
        {
            try
            {
                ObservableCollection<T> oc = new ObservableCollection<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            var val = row[prop.Name];
                            if (val == DBNull.Value)
                                propertyInfo.SetValue(obj, null, null);
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(val, propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                            //continue;
                            //break;
                            return null;
                        }
                    }
                    oc.Add(obj);
                }
                return oc;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return null;
            }
        }

        /// <summary>
        /// Convert Table To List of line items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="addAction"></param>
        /// <returns></returns>
        public static List<T> ConvertToLineItemList<T>(this DataTable table, Func<DataRow, T> addAction)
        {
            if (table == null || table.Rows == null || table.Rows.Count == 0) return null;
            List<T> lines = new List<T>();
            //
            /// Convert table rows into line items
            ////
            var datarows = table.Rows.Cast<DataRow>().ToList();
            try
            {
                foreach (var row in datarows)
                {
                    T nextLine = addAction(row);
                    if (nextLine != null)
                        lines.Add(nextLine);
                    else
                        continue;
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return null;
            }
            return lines;
        }

        /// <summary>
        /// Get Column Header Names from a given datatable instance
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<string> GetColumnHeaderNames(this DataTable table)
        {
            try
            {
                return table.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToList();
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return null;
            }
        }

        /// <summary>
        /// Set the Order of datatable columns
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnNames"></param>
        public static void SetColumnsOrder(this DataTable table, params string[] columnNames)
        {
            if (table == null) return;
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                try
                {
                    if (table.Columns[columnName] == null)
                    {
                        Debug.WriteLine($"Could not find column {columnName} in table {table.TableName}");
                        return;
                    }
                    table.Columns[columnName].SetOrdinal(columnIndex);
                    columnIndex++;
                }
                catch (Exception ex)
                {
                    string errMsg = string.Format($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                    Debug.WriteLine(errMsg);
                }
            }
        }

        /// <summary>
        /// Remove a DataTable's column by name IFF it exists!
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        public static void RemoveColumn(this DataTable table, string columnName)
        {
            try
            {
                if (table.Columns.Contains(columnName)) table.Columns.Remove(columnName);
            }
            catch (Exception ex)
            {
                string errMsg = string.Format($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}");
                Debug.WriteLine(errMsg);
            }
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> ToObjectList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            var val = row[prop.Name];
                            if (val == DBNull.Value)
                                propertyInfo.SetValue(obj, null, null);
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(val, propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                            //continue;
                            //break;
                            return null;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return null;
            }
        }

        /// <summary>
        /// As Dynamic Enumerable
        /// Turns a datatable into a dynamic enumerable type
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..
            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;
            internal DynamicRow(DataRow row) { _row = row; }
            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }
        /**** SQL To Datatable ***/
        /// <summary>
        /// Fill a given DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="connStr"></param>
        /// <param name="selectQuery"></param>
        private static void FillTable(this DataTable dt, string connStr, string selectQuery)
        {
            Debug.WriteLine(selectQuery);
            Debug.WriteLine(connStr);
            using (SqlDataAdapter da = new SqlDataAdapter(selectQuery.ToString(), connStr))
            {
                da.SelectCommand.CommandTimeout = 180; //make into a Setting?
                try
                {
                    da.Fill(dt);
                    Debug.WriteLine(dt.Rows.Count);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                    throw; //replace with a decent exception
                }
            }
        }
    }
}
