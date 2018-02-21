using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Common.Extensions.Tests
{
    [TestClass()]
    public class DynamicObjectExtensionsTests
    {
        private string xml;
        private XDocument document;
        private ExpandoObject myexpando;
        dynamic basicDynamic = new { Name = "Mike", Age = 23/*, value3 = "value" */};

        [TestInitialize]
        public void Initialize()
        {
            xml = "<Person><Name>Mike</Name><Age>23</Age><FavPony>FizzlePop</FavPony></Person>";
            Debug.WriteLine(xml);
            document = XDocument.Parse(xml);
            myexpando = document.ToExpando();
        }

        [TestMethod()]
        public void FromExpandoTreeTest()
        {
            var value = basicDynamic.GetType().GetProperty("Name").GetValue(basicDynamic, null);
            Debug.WriteLine(((object)value).ToString());

            Person person1 = myexpando.ToInstance<Person>();
            Person person2 = myexpando.ToInstance<Person>();

            person1.Dump();
            person2.Dump();
        }

        [TestMethod]
        public void DynamicToDataTable()
        {
            var contacts = new List<dynamic>();

            contacts.Add(new ExpandoObject());
            contacts[0].Name = "Patrick Hines";
            contacts[0].Phone = "206-555-0144";

            contacts.Add(new ExpandoObject());
            contacts[1].Name = "Ellen Adams";
            contacts[1].Phone = "206-555-0155";
            var table1 = DynamicObjectExtensions.ToDataTable(contacts);

            var table2 = DynamicObjectExtensions.ToDataTable(new List<dynamic>(myexpando as dynamic));
        }

        [TestMethod]
        public void NaturalDynamicToClass()
        {
            Person person = new Person
            {
                Name = "Mike",
                Age = 23,
            };

            var dyn = person.ToDynamic();
        }

        [TestMethod]
        public void XmlToDynamicToClassInstance()
        {
            //Person example = new Person { Name = "x", Age = 100 };
            //var person = (basicDynamic as ExpandoObject).ConvertTo(example);
            //var person = (basicDynamic as ExpandoObject).ConvertTo<Person>();
            //person.Dump();

            //var expando = new ExpandoObject();
            //dynamic dynamicExpando = expando;
            //dynamic dynamicExpando = myexpando;
            //dynamicExpando.Foo = "SomeString";
            //dynamicExpando.Bar = 156;
            //var result = expando.ConvertTo(new { Foo = "", Bar = 1 });

            XDocument doc = XDocument.Parse(xml); //or XDocument.Load(path)
            //dynamic dyn = XmlWrapper.Convert(doc.Root);
            string jsonText = JsonConvert.SerializeXNode(doc);
            dynamic dyn = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
            //dyn.Name = "Mike";

            //var result = (dyn as ExpandoObject).ConvertTo(new { Name = "", Age = 1 });
            dyn.BattingAvg = 10;
            IDictionary<string, object> dictionary = dyn;
            //var person = new Person();
            //DynamicObjectExtensions.Slurp(person, dyn);
            var person = ConvertToInstanceOf<Person>(dictionary);
            person.Dump("person");
        }

        public T ConvertToInstanceOf<T>(IDictionary<string, object> dictionary) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var instance = Activator.CreateInstance<T>();

            foreach (var pair in dictionary ?? new Dictionary<string, object>(0))
            {
                IDictionary<string, object> subdictionary = pair.Value as ExpandoObject;

                foreach (var item in subdictionary ?? new Dictionary<string, object>(0))
                {
                    //item.Dump("kvp");

                    //if(primitivetype)
                    var property = type.GetProperty(item.Key);

                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(instance, TypeDescriptor.GetConverter(property.PropertyType)
                            .ConvertFrom(item.Value), null);

                    //if(classtype)
                    //go deeper!

                    //property.SetValue(instance, item.Value);
                }
            }

            return instance;
        }

    }

    [TestClass]
    public class Stage1DynamicExtensionsTests
    {
        [TestInitialize]
        public void Init()
        {

        }
    }

    public class XmlWrapper
    {
        public static dynamic Convert(XElement parent)
        {
            dynamic output = new ExpandoObject();

            output.Name = parent.Name.LocalName;
            output.Value = parent.Value;

            output.HasAttributes = parent.HasAttributes;
            if (parent.HasAttributes)
            {
                output.Attributes = new List<KeyValuePair<string, string>>();
                foreach (XAttribute attr in parent.Attributes())
                {
                    KeyValuePair<string, string> temp = new KeyValuePair<string, string>(attr.Name.LocalName, attr.Value);
                    output.Attributes.Add(temp);
                }
            }

            output.HasElements = parent.HasElements;
            if (parent.HasElements)
            {
                output.Elements = new List<dynamic>();
                foreach (XElement element in parent.Elements())
                {
                    dynamic temp = Convert(element);
                    output.Elements.Add(temp);
                }
            }

            return output;
        }
    }
}