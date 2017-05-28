using CardGamesAPI.Yugioh;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Common.Yugioh.Tests
{
    [TestClass()]
    public class DatabaseServiceBaseTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            MonsterCard monster = new MonsterCard("Dark Magician", YugiohMonsterAttribute.Dark, YugiohMonsterType.Spellcaster, YugiohMonsterBaseType.Normal);
            string connectionString = "Data Source=NICK-PC;Initial Catalog=Yugioh;Integrated Security=True;";
            monster.Attack = 3000;
            //monster.CardBaseType = YugiohCardBaseType.Spell;
            CardDatabaseService service = new CardDatabaseService(connectionString);
            Assert.IsTrue(service.Insert(monster));
            monster.Dump();
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