using CardGamesAPI.Yugioh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Common.Interfaces.Tests
{
    [TestClass()]
    public class DatabaseServiceBaseTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            MonsterCard monster = new MonsterCard("BEWD", "Light", "Dragon");
            string connectionString =
      "Persist Security Info=false;Integrated Security=false;Server=Nick-PC;Database=Yugioh;User ID=;Password=";


            //DatabaseService service = new DatabaseServiceBase(connectionString);


        }
        [TestMethod()]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }
        [TestMethod()]
        public void SearchTest()
        {
            throw new NotImplementedException();
        }
        [TestMethod()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }
        [TestMethod()]
        public void SetConnectionStringTest()
        {
            throw new NotImplementedException();
        }
    }
}