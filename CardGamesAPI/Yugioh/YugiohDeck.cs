using System;
using System.Collections.Generic;
using CardGames;
using CardGamesAPI.Constants;
namespace CardGamesAPI.Yugioh
{
    public class YugiohDeck : DeckBase
    {
        const int DefaultSize = YugiohConstants.YugiohMinDeckSize;
        public override IEnumerable<ICard> Cards { get; set; }
        public override void Draw(int numberOfCards = 1)
        {
            throw new NotImplementedException();
        }
        public override void Shuffle()
        {
            throw new NotImplementedException();
        }
    }
}
