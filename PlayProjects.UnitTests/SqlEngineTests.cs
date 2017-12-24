using CardGamesAPI.Yugioh;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessLayers.Tests
{
    [TestClass()]
    public class SqlEngineTests
    {
        string connectionString = PlayProjects.UnitTests.Properties.Settings.Default.YugiohConnectionString;
        Rule _rule;
        IYugiohCard _card;

        [TestInitialize]
        public void Init()
        {
            _rule = new Rule
            {
                PreConditions = "TEST",
                PostConditions = "test",
                Name = "test",
                Type = "test",
                Description = "test2"
            };

            _card = (SynchroMonster)YugiohCardFactory.CreateCard("Stardust Dragon", YugiohMonsterCardType.Synchro);
        }

        [TestMethod()]
        public void Can_Perform_Imperative_Insert()
        {
            //Note: having an issue with Insert query not accepting param @vars;  Run the test to see what I mean.
            DummyDAL cardsDAL = new DummyDAL(connectionString, "Cards");
            cardsDAL.Insert((SynchroMonster)_card);

            DummyDAL rulesDAL = new DummyDAL(connectionString, "Rules");
            rulesDAL.Insert(_rule);


        }

        public void Can_Perform_Imperative_Delete()
        {

        }

    }

    class DummyDAL : DataAccess
    {
        public DummyDAL(string connectionString, string tableName)
            : base(connectionString, tableName)
        {
        }

    }

    class Rule : IRule
    {
        public string PreConditions { get; set; }
        public string PostConditions { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}