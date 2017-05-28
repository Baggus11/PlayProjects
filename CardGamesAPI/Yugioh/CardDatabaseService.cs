using Common.Interfaces;
namespace CardGamesAPI.Yugioh
{
    class DatabaseService : DatabaseServiceBase
    {
        //todo Create a trading card identifier base class that contains all necessary properties, then differentiate that class according to type (yugioh, magic, pokemon, standard, poker, bakarat, etc.)
        public DatabaseService(string connectionString) : base(connectionString)
        {
        }
    }
}
