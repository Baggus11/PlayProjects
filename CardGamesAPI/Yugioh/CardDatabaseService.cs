using Common;
namespace CardGamesAPI.Yugioh
{
    public class CardDatabaseService : DatabaseServiceBase
    {
        public CardDatabaseService(string connectionString)
            : base(connectionString, "Cards")
        {
        }
    }
}
