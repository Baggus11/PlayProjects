using CardGames;
using System.Collections.Generic;

namespace CardGamesAPI
{
    public interface IDeck
    {
        IEnumerable<ICard> Cards { get; set; }

        void Shuffle();

        void Draw(int numberOfCards = 1);

    }
}
