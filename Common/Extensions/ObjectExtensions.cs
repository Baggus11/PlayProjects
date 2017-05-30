using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Dump an object's properties to Debug in JSON format
        /// Note: JSON nuget package required
        /// Null values will be ignored by default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Instance of any passed object</param>
        /// <param name="displayName">Custom Name of passed object</param>        
        /// <returns></returns>
        public static T Dump<T>(this T obj, string displayName = "", bool ignoreNulls = false)
        {
            if (obj != null)
            {
                if (string.IsNullOrWhiteSpace(displayName))
                    displayName = obj.GetType().Name;
                var prettyJson = JsonConvert.SerializeObject(
                    obj,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() },
                        NullValueHandling = (ignoreNulls) ? NullValueHandling.Ignore : NullValueHandling.Include,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                Debug.WriteLine(string.Format("{0}:\n{1}", displayName, prettyJson));
            }
            else if (obj == null)
            {
                if (!string.IsNullOrWhiteSpace(displayName))
                    Debug.WriteLine(string.Format("Object '{0}'{1}", displayName, " is null.")); //Optional
            }
            return obj;
        }
        public static bool JsonCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;
            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);
            return objJson == anotherJson;
        }
        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;
            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (!objValue.Equals(anotherValue)) result = false;
            }
            return result;
        }
        public static bool Compare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;
            //properties: int, double, DateTime, etc, not class
            if (!obj.GetType().IsClass) return obj.Equals(another);
            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                //Recursion
                if (!objValue.DeepCompare(anotherValue)) result = false;
            }
            return result;
        }
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            try
            {
                return obj.GetType().GetProperties()
                    ?.Single(pi => pi.Name == propertyName)
                    ?.GetValue(obj, null);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()} for property '{propertyName}'");
                return null;
            }
        }
        /// <summary>
        /// Check an object's properties against a given table's schema
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool MatchesTable<T>(this T obj, string tableName, string connectionString)
        {
            throw new NotImplementedException();
            var properties = typeof(T).GetProperties();
            bool result = true;
            if (connectionString.IsConnectionString())
            {
                foreach (var parameter in connectionString.GetSqlParams(tableName))
                {
                    //check each property
                    //find a fail, then:
                    result = false;
                }
            }
            return result;
        }
    }
}
