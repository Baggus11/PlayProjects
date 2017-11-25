namespace CardGamesAPI.Yugioh
{
    public class TrapFactory : YugiohCardFactory
    {
        public TrapFactory()
        {
            prototype = new TrapCard();
        }

        public override IYugiohCard BuildDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}
