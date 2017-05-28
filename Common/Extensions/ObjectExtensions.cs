using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
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
    }
}
