using CardGamesAPI.Yugioh.Classes;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Factories
{
    public class TrapFactory : YugiohCardFactoryBase
    {
        public TrapFactory()
        {
            cardPrototype = new TrapCard();
        }
    }
}
