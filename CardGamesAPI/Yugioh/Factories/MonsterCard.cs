namespace CardGamesAPI.Yugioh
{
    //Concrete Prototype class
    public class MonsterCard : MonsterCardBase
    {
        public MonsterCard() { }

        public MonsterCard(string name) : base(name) { }

        public MonsterCard(string name, YugiohMonsterCardType type)
            : base(name, type) { }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
