using CardGamesAPI.Yugioh.Classes;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Factories
{
    public class SpellFactory : YugiohCardFactoryBase
    {
        public SpellFactory()
        {
            cardPrototype = new SpellCard();
        }
    }
}
