using System;

namespace CardGamesAPI.Yugioh
{
    public class SpellCard : SpellCardBase
    {
        public SpellCard(string name) : base(name) { }

        public SpellCard(string name, YugiohSpellType type, int? speed) : base(name, type, speed) { }

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
