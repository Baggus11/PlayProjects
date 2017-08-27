using CardGames;
using System;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public abstract class CardEffectBase : IEffect
    {
        public string Text { get; set; }

        public IEnumerable<Func<bool, ICard, ICard>> EffectActions { get; set; }

        public abstract YugiohMove Activate(YugiohMove currentState);
    }
}
