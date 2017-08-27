using System;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    /// <summary>
    /// Any derived classes will have their own implemenation
    /// </summary>
    public abstract class EffectServiceBase : IEffectService
    {
        public IMove MoveState { get; set; }

        public abstract bool Activate();

        public abstract bool Activate<T>(IEnumerable<Func<bool, T>> actions);

        public abstract bool Activate<T>(Func<bool, T> action);

        public abstract Func<bool, T> CompileAction<T>(IEffect effect);
    }
}
