using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extract Object
        /// Gets all the properties of T and searches the given text for matches.
        /// The accompanying Regex pattern must contain named groups for the properties to be assigned any values
        /// (optional) Warning shown if the property count DNE the Groups.Count
        ///     Note: If warning is deferred, this method will leave properties it could not find 
        ///         matched groups for with NULL values by default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <param name="regexPattern"></param>
        /// <param name="matchExact">
        ///     if true, warns if unequal Group and Property counts (must be exactly the same to proceed!)
        ///     if false, sets as many Group values to their respective properties (despite both Counts)</param>
        /// <param name="showWarnings">If set to true, warnings and debug trace lines will be printed for each object (advanced)</param>
        /// <returns></returns>		
        public static T ExtractObject<T>(this string text, string regexPattern, bool matchExact = true, bool showWarnings = true)
        {
            var dfltObj = default(T); //default ("null") return value;
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (string.IsNullOrWhiteSpace(text))
                    return dfltObj;
                Regex regex = new Regex(regexPattern, RegexOptions.Singleline);
                Match match = regex.Match(text);
                if (!match.Success)
                {
                    if (showWarnings)
                    {
                        Debug.WriteLine($"No matches found! Could not extract a '{typeof(T).Name}' instance from regex pattern:\n{regexPattern}.\n");
                        Debug.WriteLine(text);
                        Debug.WriteLine("Properties without a mapped Group:");
                        properties.Select(p => p.Name).ToList()
                                  .Except(regex.GetGroupNames().ToList())
                                  .ToList().ForEach(l => Debug.Write(l + '\t'));
                        Debug.WriteLine("\n");
                    }
                    return dfltObj;
                }
                //
                /// If the user cares to match ALL parsed groups
                /// to their respective properties:
                ////
                else if (matchExact && match.Groups.Count - 1 != properties.Length) //Optional
                {
                    if (showWarnings)
                    {
                        Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name}() WARNING: Number of Matched Groups ({match.Groups.Count}) does not equal the number of properties for the given class '{typeof(T).Name}'({typeof(T).GetProperties().Length})!  Check the class type and regex pattern for errors and try again.");
                    }
                    Debug.WriteLine("Values Parsed:");
                    for (int i = 1; i < match.Groups.Count; i++)
                        Debug.Write($"{match.Groups[i].Value}\t");
                    Debug.WriteLine("\n");
                    if (matchExact)
                    {
                        Debug.WriteLine($"Could not create an exact match! Returning default {typeof(T).Name}");
                        return dfltObj;
                    }
                }
                //
                /// If the user does not care for an exact match 
                /// and will take whatever gets parsed (correctly):
                ////
                object instance = Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo prop in properties) //Assign matching group values to new instance
                {
                    string value = match?.Groups[prop.Name]?.Value?.Trim();
                    if (!string.IsNullOrWhiteSpace(value))
                        prop.SetValue(instance, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(value), null);
                    else prop.SetValue(instance, null, null);
                }
                return (T)instance; //goal
            }
            catch (Exception ex)
            {
                string errMsg = $"{MethodBase.GetCurrentMethod().Name}: {ex.ToString()}";
                Debug.WriteLine(errMsg);
                return dfltObj;
            }
        }
        /// <summary>
        /// ExtractPrimitives
        ///
        /// Extracts all fields from a string that match a certain regex. 
        /// Will convert to desired type through a standard TypeConverter.
        /// Supports basic primative types ONLY
        /// Tip: Extract the 'T' type you expect (like int) to retrieve;
        /// (default to string if unsure)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static T[] ExtractPrimitives<T>(this string text, string regexPattern)
        {
            try
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                if (!tc.CanConvertFrom(typeof(string)))
                {
                    throw new ArgumentException("Type does not have a TypeConverter from string", "T");
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    return
                        Regex.Matches(text, regexPattern)
                        .Cast<Match>()
                        .Select(f => f.ToString())
                        .Select(f => (T)tc.ConvertFrom(f))
                        .ToArray();
                }
                else
                    return new T[0];
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
                return new T[0];
            }
        }
        /// <summary>
        /// Convert To Enum
        /// Converts a given string to the matching enum type T!.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException($"{MethodBase.GetCurrentMethod().Name}> Could not convert string '{value}' to type {typeof(T).Name}");
            else
                return (T)Enum.Parse(typeof(T), value);
            //return (T)Convert.ChangeType(value, typeof(T));
        }
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);
        /// <summary>
        /// Extract Nested Functions and Params
        /// </summary>
        public static MatchCollection ExtractNestedFunctionsAndParams(this string txt)
        {
            string rgx = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";
            var match = Regex.Match(txt, rgx);
            string innerArgs = match.Groups[1].Value;
            MatchCollection matches = Regex.Matches(innerArgs, rgx);
            return matches;
        }
    }
}
