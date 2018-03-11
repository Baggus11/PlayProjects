using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
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
            myexpando = document.ToDynamic();
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
    }

    [TestClass]
    public class Stage1DynamicExtensionsTests
    {
        private string xml;
        private string xmlFileName = "Effect.xml";
        private Assembly assembly = Assembly.GetExecutingAssembly();

        [TestInitialize]
        public void Initialize()
        {
            xml = assembly.GetEmbeddedResourceContent(xmlFileName);
        }

        [TestMethod]
        public void EffectXmlToInstanceTest()
        {
            //Assemble
            Debug.WriteLine(xml);

            //Act
            var expando = XDocument.Parse(xml).ToDynamic() as ExpandoObject;
            var effect = expando.ToInstance<CardEffect>();
            effect.Dump(nameof(effect));

            //Assert
            Assert.IsNotNull(effect);
        }

        //test poco
        public class CardEffect
        {
            public string EffectText { get; set; }
            public int SpellSpeed { get; set; }
            public EffectOption Option { get; set; }
        }

        public class Player
        {
            public string Name { get; set; }
            public int LifePoints { get; set; }
        }

        public class EffectOption
        {
            public string Text { get; set; }
            public int Cost { get; set; }
            public Player Target { get; set; }
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