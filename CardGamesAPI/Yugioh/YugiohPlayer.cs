using CardGamesAPI.Interfaces;
using Common;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public class YugiohPlayer : Player
    {
        public List<IYugiohCard> Deck = new List<IYugiohCard>();
        public List<IYugiohCard> Hand = new List<IYugiohCard>();
        public List<IYugiohCard> MonsterZone = new List<IYugiohCard>();
        public List<IYugiohCard> SpellZone = new List<IYugiohCard>();
        public IYugiohCard FieldSpell;

        public YugiohPlayer(string playerName = "not set")
            : base(playerName)
        {
            Score = 8000;
            Name = playerName;
        }

        public YugiohPlayer(string playerName, int lifePoints)
            : this(playerName)
        {
            Score = lifePoints;
        }

        public override void Draw(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
                Hand.Add(Deck.PopAt(0));
        }

    }
}
