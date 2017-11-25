namespace CardGamesAPI.Yugioh
{
    //Concrete Prototype class
    public class SpellCard : SpellCardBase
    {
        public SpellCard() { }

        public SpellCard(string name) : base(name) { }

        public SpellCard(string name, YugiohSpellCardType type, int? speed) : base(name, type, speed) { }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

}
