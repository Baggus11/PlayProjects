﻿using System;
using System.Collections.Generic;
namespace CardGamesAPI
{
    //Contract for an ICard to hold an effect
    public interface IEffect
    {
        string EffectRawText { get; }
        //IEnumerable<Func<bool, object>> EffectActions { get; set; } //post-translation.  An EffectService will initialize these!
        //                                                            //TODO: your translated effects from your Rules Engine will be assigned to a property from this interface
    }
}
