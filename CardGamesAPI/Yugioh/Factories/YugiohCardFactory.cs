namespace CardGamesAPI.Yugioh
{
    public abstract class YugiohCardFactory
    {
        private static MonsterFactory _monsterFactory = new MonsterFactory();
        private static SpellFactory _spellFactory = new SpellFactory();
        private static TrapFactory _trapFactory = new TrapFactory();

        public IYugiohCard prototype;

        public static YugiohCardFactory GetFactory(YugiohCardType cardType)
        {
            YugiohCardFactory factory = null;

            switch (cardType)
            {
                case YugiohCardType.Monster:
                    factory = _monsterFactory;
                    break;
                case YugiohCardType.Spell:
                    factory = _spellFactory;
                    break;
                case YugiohCardType.Trap:
                    factory = _trapFactory;
                    break;
                default:
                    break;
            }

            return factory;
        }

        public IYugiohCard CreateCard()
        {
            return (IYugiohCard)prototype.Clone();
        }

        //todo: builder pattern to construct card from details (struct) passed to it:
        public abstract IYugiohCard BuildDetails();


        //public static IYugiohCard CreateCard(string cardName)
        //{
        //    throw new NotImplementedException(); //todo: assemble from database by name
        //    //If cannot find stats in db, set all known properties to null (so programmer knows what's up).
        //    //I will create a var type that holds known properties and pass those onto the builder.

        //}

        //public static IYugiohCard CreateCard<TEnum>(string cardName, TEnum specificCardType)
        //    where TEnum : struct, IConvertible, IFormattable, IComparable
        //{         


        //if (!typeof(TEnum).IsEnum)
        //    throw new ArgumentException("TEnum must be an enumerated type");

        //YugiohCardType baseType = GetCardBaseTypeFromSpecificType(specificCardType);

        ////Using this abstract factory to create the 3 base type of factories:
        //var @switch = new Dictionary<YugiohCardType, Func<string, TEnum, IYugiohCard>>
        //{
        //    { YugiohCardType.MonsterCard,(name, type) => { return MonsterFactory.CreateMonster(cardName, specificCardType); } },
        //    { YugiohCardType.SpellCard,(name, type) => { return SpellFactory.CreateSpell(cardName, specificCardType); } },
        //    { YugiohCardType.TrapCard,(name, type) => { return MonsterFactory.CreateTrapCard(cardName, specificCardType); } }

        //};

        //return @switch[baseType](cardName, specificCardType);

        //}

        //public static IYugiohCard CreateCardOfType<TBase>(string cardName)
        //    where TBase : class, IYugiohCard, new()
        //{
        //    var @interface = typeof(TBase);

        //    try
        //    {
        //        throw new NotImplementedException("The same implementation you created for enums, create for Interfaces!");
        //        var @switch = new Dictionary<Type, Func<string, IYugiohCard>>()
        //        {
        //            //Todo: The same implementation you created for enums, create for Interfaces:
        //            //{typeof(IMonsterCard), (name) => { return MonsterFactory.CreateMonster(name, @interface); } },
        //            //{typeof(ISpellCard), (name) => { return SpellFactory.CreateSpell(name, @interface); } },
        //            //{typeof(ITrapCard), (name) => { return TrapFactory.CreateMonster(name, @interface); } }

        //        };

        //        return @switch[@interface](cardName);

        //    }
        //    catch (Exception)
        //    {
        //        throw;

        //    }
        //}

        //protected static object GetYugiohCardDetails(string cardName)
        //{
        //    //todo: 1) find all possible values from whatever DB table we point this to.
        //    //2) if we know the card base type and specific type, we can infer most of the rest of the details!  So do that by use of Reflection and factories!
        //    throw new NotImplementedException();
        //}      

        //public static IYugiohCard CreateRandomCard()
        //{
        //    //todo: pick random type and params object[]s, then create call CreateCard(name,base,values) on that.  Unrelated values will be 'sifted' or ignored.
        //    throw new NotImplementedException();
        //}

        //public static IYugiohCard CreateRandomCardType()
        //{
        //    var type = typeof(IYugiohCard).Assembly.GetTypes()
        //        //Only using interfaces atm.
        //        .Where(t => t.IsInterface)
        //        .Where(p => typeof(IYugiohCard).IsAssignableFrom(p))
        //        .TakeFirstRandom();

        //    return CreateRandomCard(type);

        //}

        //public static IYugiohCard CreateRandomCard<T>()
        //    where T : class, IYugiohCard, new()
        //{
        //    return CreateRandomCard(typeof(T));
        //}

        //private static IYugiohCard CreateRandomCard(Type type)
        //{
        //    try
        //    {
        //        var @switch = new Dictionary<Type, Func<IYugiohCard>>()
        //        {
        //            {typeof(IMonsterCard), ()=> { return MonsterFactory.CreateRandomMonster(); } },
        //            {typeof(ISpellCard), ()=> { return SpellFactory.CreateRandomSpell(); } },
        //            {typeof(ITrapCard), ()=> { return TrapFactory.CreateRandomTrap(); } },

        //        };

        //        return @switch[type]();

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
