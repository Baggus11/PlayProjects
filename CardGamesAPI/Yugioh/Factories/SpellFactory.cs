namespace CardGamesAPI.Yugioh
{
    public class SpellFactory : YugiohCardFactory
    {
        public SpellFactory()
        {
            prototype = new SpellCard();
        }

        public override IYugiohCard BuildDetails()
        {
            throw new System.NotImplementedException();
        }



        //public static ISpellCard CreateRandomSpell()
        //{
        //    var spelltype = EnumExtensions.GetRandomEnumValue<YugiohSpellCardType>();
        //    int spellspeed = Enumerable.Range(1, 3).TakeFirstRandom();

        //    return new SpellCard("random spell");

        //}

        //internal static IYugiohCard CreateSpell<TEnum>(string cardName, TEnum specificCardType)
        //    where TEnum : struct, IConvertible, IFormattable, IComparable
        //{
        //    throw new NotImplementedException();
        //}
    }

}
