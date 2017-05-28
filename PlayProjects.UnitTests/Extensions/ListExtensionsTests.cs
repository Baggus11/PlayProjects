using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayProjects.UnitTests;
using System;
using System.Collections.Generic;
using System.Data;

namespace Common.Extensions.Tests
{
    [TestClass()]
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
        [TestMethod()]
        public void AppendTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SwapTest()
        {
            throw new NotImplementedException();

        }

        [TestMethod()]
        public void ToDatatableTest()
        {
            throw new NotImplementedException();

        }
    }
}