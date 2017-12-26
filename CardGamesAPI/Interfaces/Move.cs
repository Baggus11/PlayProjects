using System.Collections.Generic;

namespace CardGamesAPI.Interfaces
{
    public abstract class Move : IMove
    {
        public virtual IPlayer TurnPlayer { get; set; }
        public virtual IPlayer Opponent { get; set; }
        public List<IMove> Children { get; set; }
        public int TurnNumber { get; set; }

        public void Dispose()
        {
            if (Children?.Count > 0)
            {
                foreach (var item in Children)
                {
                    item.Dispose();
                }
            }

            TurnPlayer = null;
            Opponent = null;
        }

        public abstract int GetScoreOf(IPlayer player);

        public abstract int GetScore(bool turnPlayer);

        public abstract bool IsTerminal(bool turnPlayer);

    }

}
