namespace CardGamesAPI.Yugioh.Implementors
{
    /// <summary>
    /// Implements Card 'bridge'
    /// </summary>
    public class BasicCardImplementation : YugiohCardBaseImplementation
    {
        public BasicCardImplementation()
            : base(null)
        //: base(new YugiohCard() as IYugiohCard) //caused casting issues

        { }

    }
}
