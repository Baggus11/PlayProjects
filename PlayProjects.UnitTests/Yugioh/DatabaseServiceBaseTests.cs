using CardGamesAPI.Yugioh;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Common.Yugioh.Tests
{
    [TestClass()]
    public class DatabaseServiceBaseTests
    {
        private string connectionString = "Data Source=NICK-PC;Initial Catalog=Yugioh;Integrated Security=True;";
        CardDatabaseService cardService;
        private List<MonsterCard> monsterList = new List<MonsterCard>();
        public DatabaseServiceBaseTests()
        {
            cardService = new CardDatabaseService(connectionString);
            monsterList.Add(new MonsterCard("Dark Magician", YugiohMonsterAttribute.Dark, YugiohMonsterType.Fairy, YugiohMonsterBaseType.Normal));
            monsterList.Add(new MonsterCard("Silent Magician Lvl 4", YugiohMonsterAttribute.Light, YugiohMonsterType.Fairy, YugiohMonsterBaseType.Effect));
            monsterList.Dump();
        }
        [TestMethod()]
        public void Insert_Item()
        {
            Assert.IsTrue(cardService.Insert(monsterList.First()));
        }
        [TestMethod]
        public void Insert_Collection()
        {
            Assert.IsTrue(cardService.InsertCollection(monsterList));
        }
        [TestMethod()]
        public void DeleteItem()
        {
            Assert.IsTrue(cardService.Delete(monsterList.First()));
        }
        [TestMethod]
        public void DeleteItems()
        {
            Assert.IsTrue(cardService.DeleteCollection(monsterList));
        }
        [TestMethod()]
        public void SearchItem()
        {
            throw new NotImplementedException();
        }
        [TestMethod()]
        public void UpdateItem()
        {
            throw new NotImplementedException();
        }
    }
}