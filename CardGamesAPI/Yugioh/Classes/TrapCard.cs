using System;

namespace CardGamesAPI.Yugioh
{
    public class TrapCard : TrapCardBase
    {
        public TrapCard() { }

        public TrapCard(string name) : base(name) { }

        public TrapCard(string name, YugiohTrapCardType type, int? speed) : base(name, type, speed) { }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

    }

}
