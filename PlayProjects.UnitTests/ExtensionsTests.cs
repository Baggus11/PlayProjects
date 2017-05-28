using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;

namespace PlayProjects.UnitTests
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void List_To_Datatable()
        {
            //Arrange
            DataTable dt;
            List<Person> list = new List<Person> {
                new Person { Name = "Luna", Age = 1021 },
                new Person { Name = "Celestia", Age = 1028 }
            };

            //Act
            list.Dump("list");
            dt = list.ToDatatable();
            dt.Dump("table");
            //Assert
            Assert.IsTrue(dt.Rows.Count > 0 && dt.Rows.Count == list.Count);
        }
    }
}
