using System;

namespace CardGamesAPI.Yugioh
{
    public class TrapCard : TrapCardBase
    {
        public TrapCard(string name) : base(name) { }

        public TrapCard(string name, YugiohTrapType type, int? speed) : base(name, type, speed) { }

        public override YugiohMove Activate(YugiohMove currentState)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

    }

}
