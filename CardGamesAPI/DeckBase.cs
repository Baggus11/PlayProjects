using System.Collections.Generic;
using CardGames;
namespace CardGamesAPI
{
    public abstract class DeckBase : IDeck
    {
        public abstract IEnumerable<ICard> Cards { get; set; }
        public abstract void Draw(int numberOfCards = 1);
        public abstract void Shuffle();
    }
}
