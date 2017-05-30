using System;
using System.Collections.Generic;
namespace CardGamesAPI
{

    /// <summary>
    /// Todo - plug in your old Rules Engine here; Implement in an abstract class elsewhere :)
    /// </summary>
    public interface IEffectService
    {
        IGameState State { get; set; }//hopefully this will be a move state once I'm done implementing
        bool Activate(); //Activate a stored effect on State
        bool Activate<T>(Func<bool, T> action); //one-way action
        bool Activate<T>(IEnumerable<Func<bool, T>> actions); //one-way multiple actions
        //Compile a game effect from rules directly to func (see rules engine.cs)
        Func<bool, T> CompileAction<T>(IEffect effect);
    }
}
