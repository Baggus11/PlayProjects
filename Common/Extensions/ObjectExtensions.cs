using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Common
{
    public static class ObjectExtensions
    {
        public static void MapDerived<T, TDerived>(ref T source, ref TDerived destination)
            where T : new()
            where TDerived : new()
        {
            if (source == null) source = new T();
            if (destination == null) destination = new TDerived();

            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null!");

            Type typeDestination = destination.GetType();
            Type typeOfSource = source.GetType();

            var properties_to_map = from sourceProperty in typeOfSource.GetProperties()
                                    let targetProperty = typeDestination.GetProperty(sourceProperty.Name)
                                    where sourceProperty.CanRead
                                    && targetProperty != null
                                    && (targetProperty.GetSetMethod(true) != null
                                    && !targetProperty.GetSetMethod(true).IsPrivate)
                                    && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                                    && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)
                                    select new { sourceProperty = sourceProperty, targetProperty = targetProperty };

            foreach (var properties in properties_to_map)
            {
                properties.targetProperty.SetValue(destination, properties.sourceProperty.GetValue(source, null), null);
            }

        }

        public static void EatDerived<TParent, TDerived>(this TParent destination, TDerived source)
            where TParent : new()
            where TDerived : TParent, new()
        {
            if (source == null) source = new TDerived();
            if (destination == null) destination = new TParent();

            var detailsProperties = typeof(TDerived).GetProperties();
            var cardProperties = typeof(TParent).GetProperties();

            Type destinationType = destination.GetType();
            Type sourceType = source.GetType();

            var mappableProperties = from sourceProperty in sourceType.GetProperties()
                                     let targetProperty = destinationType.GetProperty(sourceProperty.Name)
                                     where sourceProperty.CanRead
                                     && targetProperty != null
                                     && (targetProperty.GetSetMethod(true) != null
                                     && !targetProperty.GetSetMethod(true).IsPrivate)
                                     && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                                     && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)
                                     select new { sourceProperty = sourceProperty, targetProperty = targetProperty };

            foreach (var property in mappableProperties)
            {
                property.targetProperty.SetValue(destination, property.sourceProperty.GetValue(source, null), null);
            }

        }

        public static void ToPropertyDictionary(object @object) => @object?.GetType().GetProperties()
            ?.ToDictionary(property => property.Name, property => property.GetValue(@object));

        public static void ToPropertyLookup(object @object) => @object?.GetType().GetProperties()
            ?.ToLookup(property => property.Name, property => property.GetValue(@object));

        public static object GetPropertyValue<T>(this T @object, string propertyName) => typeof(T).GetProperties()
            ?.Single(pi => pi.Name == propertyName)?.GetValue(@object, null);

        public static bool Compare(this object @object, object another)
        {
            if (ReferenceEquals(@object, another)) return true;
            if ((@object == null) || (another == null)) return false;
            if (@object.GetType() != another.GetType()) return false;
            //properties: int, double, DateTime, etc, not class
            if (!@object.GetType().IsClass) return @object.Equals(another);
            var result = true;

            foreach (var property in @object.GetType().GetProperties())
            {
                var objValue = property.GetValue(@object);
                var anotherValue = property.GetValue(another);
                //Recursion
                if (!objValue.DeepCompare(anotherValue)) result = false;
            }

            return result;

        }

        //Map properties from one instance of T to another by shape and types (not property names).
        public static T Map<T>(this T target, T source) => throw new NotImplementedException();

        //Map properties from one instance of T to another instance of U by shape and types(not property names).
        public static T Map<T, U>(this T target, U source)
            where T : class where U : class => throw new NotImplementedException();

        //Combine 2 different classes into a current intance of R.
        public static R Merge<T, U, R>(this R result, T first, U second)
            where T : class where U : class where R : class => throw new NotImplementedException();

        //Combine 2 different class instances into a new instance, R.
        public static R Merge<T, U, R>(T first, U second)
            where T : class where U : class where R : new() => throw new NotImplementedException();

        //Combine 2 different class instances into an out instance, R.
        public static R Merge<T, U, R>(T first, U second, out R result) => throw new NotImplementedException();

        public static T Dump<T>(this T obj, string displayName = null, bool showNulls = true)
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
                        NullValueHandling = (!showNulls) ? NullValueHandling.Ignore : NullValueHandling.Include,
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

    }
}
