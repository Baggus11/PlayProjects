using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace PlayProjects.UnitTests.Yugioh
{
    /// <summary>
    /// Test CRUD and wiki/yugiohprices parsed searches.
    /// Use Linq to SQL
    /// </summary>
    [TestClass]
    public class YGODatabaseTests
    {
        string connectionString = Properties.Settings.Default.YugiohConnectionString;

        [TestMethod]
        public void Linq_To_SQL_Insert()
        {
            //Assemble:
            LinqToSQLDataContext db = new LinqToSQLDataContext(connectionString);
            Card card = new Card { SysGuid = Guid.NewGuid(), CardName = "Dark Magician", CardBaseType = "Normal", Attack = 2500, Defense = 2100 };

            //Act:
            db.Cards.InsertOnSubmit(card);
            db.SubmitChanges();

            //Assert:
            var insertedCard = db.Cards.Where(e => e.Attack == 2500);
            Assert.IsTrue(insertedCard != null);
            insertedCard.Dump();
        }

        [TestMethod]
        public void Linq_To_SQL_Delete()
        {

        }

    }
}
