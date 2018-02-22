using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Extensions
{
    public static partial class DynamicObjectExtensions
    {
        private static bool _createNonPublicConstructor = true;

        public static T ToInstance<T>(this IDictionary<string, object> dictionary) where T : class
        {
            var type = typeof(T);
            //var instance = (T)ToInstance(CreateDefaultInstance<T>(), dictionary, typeof(T));
            var instance = (T)ToInstance(Activator.CreateInstance(type, _createNonPublicConstructor), dictionary, type);

            return instance;
        }

        //private static object CreateDefaultInstance(Type instanceType)
        //{
        //    var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        //    var constructor = instanceType.GetConstructor(flags, null, new Type[0], null);

        //    return constructor.Invoke(null);
        //}

        //private static T CreateDefaultInstance<T>()
        //{
        //    var instanceType = typeof(T);
        //    return (T)CreateDefaultInstance(instanceType);
        //}

        private static object ToInstance(object parent, IDictionary<string, object> dictionary, Type childType, bool isInnerClass = false)
        {
            var parentType = parent.GetType();
            var parentProperties = parentType.GetProperties();
            var childProperties = childType.GetProperties();

            foreach (var pair in dictionary ?? new Dictionary<string, object>(0))
            {
                if (pair.Value == null)
                {
                    Debug.WriteLine($"Skipped key '{pair.Key}'");
                    continue;
                }

                var valType = pair.Value.GetType();

                try
                {
                    //todo: if List of expandos, do a foreach over all expandos in list

                    //todo: if list of objects that are not defined and NOT expandos, must be a list<object>, just iterate blindly in another method that handles list of object and calls toIntance when encountering expandos.

                    //Debug.WriteLine($"key: {pair.Key.ToString()}\traw value: {pair.Value.ToString()}\ttype: {valType.ToString()}");

                    if (!valType.Name.Equals(nameof(ExpandoObject)))
                    {
                        var matchedProperty = childProperties.SingleOrDefault(prop => prop.Name.Equals(pair.Key, StringComparison.OrdinalIgnoreCase));

                        matchedProperty?.SetValue(parent, TypeDescriptor.GetConverter(matchedProperty.PropertyType)
                                .ConvertFrom(pair.Value), null);

                        continue;
                    }

                    IDictionary<string, object> subDictionary = pair.Value as ExpandoObject;
                    string propertyName = pair.Key.ToString();

                    if (propertyName.Equals(parentType.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        //object childTemplate = CreateDefaultInstance(childType);
                        object childTemplate = Activator.CreateInstance(childType, _createNonPublicConstructor);
                        object child = ToInstance(parent: childTemplate, dictionary: subDictionary, childType: childType);

                        parent = child;
                    }
                    else
                    {
                        var nextProperty = childProperties.SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                        //var nextProperty = childProperties.SingleOrDefault(p => p.PropertyType.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

                        if (nextProperty == null)
                        {
                            Debug.WriteLine($"Could not find property '{propertyName}'");
                            break;
                        }

                        var nextPropertyType = nextProperty?.PropertyType;
                        object child = Activator.CreateInstance(nextPropertyType, _createNonPublicConstructor);
                        //object child = CreateDefaultInstance(nextPropertyType);
                        object subInstance = ToInstance(child, subDictionary, nextPropertyType, true) ?? Activator.CreateInstance(nextPropertyType, _createNonPublicConstructor) /*CreateDefaultInstance(nextPropertyType)*/;

                        nextProperty.SetValue(parent, subInstance);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"key: {pair.Key.ToString()}\traw value: {pair.Value.ToString()}\ttype: {valType.ToString()}");

                    Debug.WriteLine(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                    continue;
                }
            }

            return parent;
        }


        public static IEnumerable<KeyValuePair<string, object>> ToExpandoPairs(this ExpandoObject expando)
        {
            var tree = new List<KeyValuePair<string, object>>();

            foreach (var pair in expando?.ToArray() ?? Enumerable.Empty<KeyValuePair<string, object>>())
            {
                if (pair.Value == null)
                {
                    continue;
                }

                if (pair.Value.GetType().Name.Equals("ExpandoObject"))
                {
                    tree.Add(new KeyValuePair<string, object>(pair.Key, ToExpandoPairs((ExpandoObject)pair.Value)));
                }
                else
                {
                    tree.Add(new KeyValuePair<string, object>(pair.Key, pair.Value));
                }
            }

            return tree;
        }
        public static ExpandoObject ToExpando(this XDocument document)
        {
            //todo: high overhead because json conversion is slow! Find another way to parse xdocuments to expandoObjects!
            string jsonText = JsonConvert.SerializeXNode(document);
            ExpandoObject @object = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
            return @object;
        }

        public static dynamic ToDynamic(this IDictionary<string, object> dictionary)
        {
            dynamic expando = new ExpandoObject();
            var expandoDictionary = (IDictionary<string, object>)expando;

            //todo: make recursive for objects in the dictionary with more levels.
            dictionary.ToList()
                      .ForEach(keyValue => expandoDictionary.Add(keyValue.Key, keyValue.Value));

            return expando;
        }

        public static dynamic ToDynamic<T>(this T obj)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties ?? Enumerable.Empty<PropertyInfo>())
            {
                var propertyExpression = Expression.Property(Expression.Constant(obj), propertyInfo);
                string currentValue = Expression.Lambda<Func<string>>(propertyExpression).Compile().Invoke();
                expando.Add(propertyInfo.Name.ToLower(), currentValue);
            }
            return expando as ExpandoObject;
        }

        public static DataTable ToDataTable(this List<dynamic> contacts)
        {
            DataTable table = new DataTable();
            var properties = contacts.GetType().GetProperties();
            properties = properties.ToList().GetRange(0, properties.Count() - 1).ToArray();
            properties.ToList().ForEach(p => table.Columns.Add(p.Name, typeof(string)));
            contacts.ForEach(x => table.Rows.Add(x.Name, x.Phone));
            return table;
        }

    }
    public class DynamicAliasAttribute : Attribute
    {
        private string alias;
        public string Alias { get => alias; set => alias = value; }
        public DynamicAliasAttribute(string alias)
        {
            this.alias = alias;
        }
    }
}
