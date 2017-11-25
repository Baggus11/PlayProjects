using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Common
{
    public static class StringExtensions
    {
        public static string Reverse(this string str)
            => new string(str.ToCharArray().Reverse().ToArray());

        public static string SplitCamelCase(this string str)
            => Regex.Replace(Regex.Replace(str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"),
                    @"(\p{Ll})(\P{Ll})", "$1 $2");

        public static T DeserializeFromXml<T>(this string xmlString)
            where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(xmlString))
                    return serializer.Deserialize(reader) as T;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string SerializeToXml<T>(this T @object)
            where T : class
        {
            try
            {
                var serializer = new XmlSerializer(@object.GetType());
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, @object);
                    return writer.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
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
        public static T ExtractObject<T>(this string text, string regexPattern,
            bool matchExact = true, bool showWarnings = true)
        {
            var dfltObj = default(T);

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

                //object instance = Activator.CreateInstance(typeof(T));
                object instance = Activator.CreateInstance(GetAssignableTypes<T>().FirstOrDefault());

                foreach (PropertyInfo prop in properties) //Assign matching group values to new instance
                {
                    string value = match?.Groups[prop.Name]?.Value?.Trim();
                    if (!string.IsNullOrWhiteSpace(value))
                        prop.SetValue(instance, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(value), null);
                    else prop.SetValue(instance, null, null);
                }

                return (T)instance;
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

        public static T ToEnum<T>(this string value)
        {
            return !typeof(T).IsEnum ?
                throw new NotSupportedException($"{MethodBase.GetCurrentMethod().Name}> Could not convert string '{value}' to type {typeof(T).Name}")
                : (T)Enum.Parse(typeof(T), value);
        }

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static MatchCollection ExtractNestedFunctionsAndParams(this string txt)
        {
            string nestedFunctionsPattern = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";
            var match = Regex.Match(txt, nestedFunctionsPattern);
            string innerArgs = match.Groups[1].Value;
            MatchCollection matches = Regex.Matches(innerArgs, nestedFunctionsPattern);
            return matches;
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from left</returns>
        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }

        private static Type[] GetAssignableTypes<T>()
        {
            try
            {
                Type[] assignableTypes = (from t in Assembly.Load(typeof(T).Namespace).GetExportedTypes()
                                          where !t.IsInterface && !t.IsAbstract
                                          where typeof(T).IsAssignableFrom(t)
                                          select t).ToArray();
                return assignableTypes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Alternate version of XML Deserialization:
        //public static T DeserializeXML<T>(this string xml) where T : class
        //{
        //    using (TextReader reader = new StringReader(xml))
        //        return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        //}

    }
}
