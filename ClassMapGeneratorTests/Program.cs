using Dynamics;
using System;
using System.IO;

namespace ClassMapGeneratorTests
{
    class Program
    {
        //In console because the UnitTests have trouble
        static void Main(string[] args)
        {
            try
            {
                string xmlPath = @"C:\Users\Nick\Desktop\PlayProjects\CWClassMapGenerator\porgs.xml";
                string jsonPath = @"C:\Users\Nick\Desktop\PlayProjects\CWClassMapGenerator\porgs.json";

                string xmlContent = File.ReadAllText(xmlPath);
                string jsonContent = File.ReadAllText(jsonPath);

                dynamic dynamicPorg = DynamicXml.Parse(xmlContent);
                //dynamic dynamicPorg = new Porg { Name = "Porgins", Wingspan = 2 };

                //Goal:

                Console.WriteLine(dynamicPorg.Name);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
