using CardGames;
using System;
using System.Collections.Generic;
namespace CardGamesAPI.Yugioh
{
    public abstract class CardEffectBase : IEffect
    {
        public string EffectRawText { get; }
        public IEnumerable<Func<bool, ICard, ICard>> EffectActions { get; set; } //This will be set by a service either on init or during game.
        public abstract void Activate();
    }
}
