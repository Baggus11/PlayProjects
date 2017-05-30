using System;
using System.Collections.Generic;
namespace CardGamesAPI.Yugioh
{
    public class YugiohEffectService : EffectServiceBase
    {
        public YugiohEffectService()
        {
            State = new YugiohGameState { };
        }
        public override bool Activate()
        {
            throw new NotImplementedException();
        }
        public override bool Activate<ICard>(Func<bool, ICard> cardAction)
        {
            throw new NotImplementedException();
        }
        public override bool Activate<ICard>(IEnumerable<Func<bool, ICard>> cardActions)
        {
            throw new NotImplementedException();
        }
        public override Func<bool, T> CompileAction<T>(IEffect effect)
        {
            throw new NotImplementedException();
        }
    }
}
