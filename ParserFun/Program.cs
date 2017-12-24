using System.IO;
using System.Web.Script.Serialization;

namespace ParserFun
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\users\Nick\Desktop\employees.json";
            string lines = File.ReadAllLines(fileName).ToString();
            Employee employee = new JavaScriptSerializer().Deserialize<Employee>(lines);
        }


    }
    internal class Employee
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

}

