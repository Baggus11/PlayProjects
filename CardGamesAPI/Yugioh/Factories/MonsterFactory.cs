namespace CardGamesAPI.Yugioh
{
    public class MonsterFactory : YugiohCardFactory
    {
        public MonsterFactory()
        {
            prototype = new MonsterCard();
        }

        public override IYugiohCard BuildDetails()
        {
            throw new System.NotImplementedException();
        }

        //public static IMonsterCard CreateMonster<TEnum>(string cardName, TEnum type)
        //    where TEnum : struct, IComparable, IFormattable, IConvertible
        //{
        //    if (!typeof(TEnum).IsEnum)
        //        throw new ArgumentException("TEnum must be an enumerated type");

        //    var @switch = new Dictionary<Enum, Func<string, IMonsterCard>>
        //    {

        //TODO: REPLACE ALL THESE TINY FACTORIES WITH A BUILDER PATTERN THAT CREATES EACH NECESSARY PIECE OF THE CLASS, ATK,DEF, RANK, LVL, TRIBUTES, EFFECT STRING (DB), MATERIALS, ETC.
        //{ YugiohMonsterCardType.Fusion, (name) => { return FusionMonsterFactory.CreateFusionMonster(name); } },
        //{ YugiohMonsterCardType.Ritual, (name) => { return RitualMonsterFactory.CreateRitualMonster(name); } },
        //{ YugiohMonsterCardType.Synchro, (name) => { return SynchroMonsterFactory.CreateSynchroMonster(name); } },
        //{ YugiohMonsterCardType.Normal, (name) => { return NormalMonsterFactory.CreateNormalMonster(name); } },
        //{ YugiohMonsterCardType.Tuner, (name) => { return TunerMonsterFactory.CreateTunerMonster(name); } },
        //{ YugiohMonsterCardType.XYZ, (name) => { return XYZMonsterFactory.CreateXYZMonster(name); } },
        //{ YugiohMonsterCardType.Gemini, (name) => { return GeminiMonsterFactory.CreateGeminiMonster(name); } },
        //{ YugiohMonsterCardType.FlipEffect, (name) => { return FlipEffectMonsterFactory.CreateFlipEffectMonster(name); } },
        //{ YugiohMonsterCardType.Pendulum, (name) => { return PendulumMonsterFactory.CreatePendulumMonster(name); } },
        //{ YugiohMonsterCardType.Effect, (name) => { return EffectMonsterFactory.CreateEffectMonster(name); } }

        //    };

        //    return @switch[type as Enum](cardName);

        //}

        //public static ITrapCard CreateTrapCard<TEnum>(string cardName, TEnum specificCardType)
        //    where TEnum : struct, IConvertible, IFormattable, IComparable
        //{
        //    throw new NotImplementedException();
        //}

        //public static IYugiohCard CreateSpellCard<TEnum>(string cardName, TEnum specificCardType)
        //    where TEnum : struct, IConvertible, IFormattable, IComparable
        //{
        //    throw new NotImplementedException();
        //}

        //public static IMonsterCard CreateRandomMonster()
        //{
        //    int attack = Enumerable.Range(1, 30).Select(x => x * 100).TakeFirstRandom();
        //    int defense = Enumerable.Range(0, 25).Select(x => x * 100).TakeFirstRandom();

        //    int level = Enumerable.Range(1, 12).TakeFirstRandom();

        //    var monster = new NormalMonster("Token", attack, defense)
        //    {
        //        MonsterAttribute = EnumExtensions.GetRandomEnumValue<YugiohMonsterAttribute>(),
        //        MonsterCardType = EnumExtensions.GetRandomEnumValue<YugiohMonsterCardType>(),
        //        MonsterType = EnumExtensions.GetRandomEnumValue<YugiohMonsterType>()
        //    };

        //    return monster;

        //}

        //public static ISpellCard CreateRandomSpell()
        //{
        //    throw new NotImplementedException();
        //}

        //public static ITrapCard CreateRandomTrap()
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public static class PendulumMonsterFactory//:MonsterFactory
    //{
    //    internal static IMonsterCard CreatePendulumMonster(string name)
    //    {
    //        return new PendulumMonster(name);
    //    }
    //}

    //public class SynchroMonsterFactory//:MonsterFactory
    //{
    //    public static IMonsterCard CreateSynchroMonster(string name)
    //    {
    //        return new SynchroMonster(name);
    //    }
    //}

    //public class NormalMonsterFactory //: MonsterFactory
    //{
    //    public static IMonsterCard CreateNormalMonster(string name)
    //    {
    //        return new NormalMonster(name);
    //    }
    //}

    //public class TunerMonsterFactory// : MonsterFactory
    //{
    //    public static IMonsterCard CreateTunerMonster(string name)
    //    {
    //        return new TunerMonster(name);
    //    }
    //}

    //public class XYZMonsterFactory //: MonsterFactory
    //{
    //    public static IMonsterCard CreateXYZMonster(string name)
    //    {
    //        return new XYZMonster(name);
    //    }
    //}

    //public class FlipEffectMonsterFactory //: MonsterFactory
    //{
    //    public static IMonsterCard CreateFlipEffectMonster(string name)
    //    {
    //        return new FlipEffectMonster(name);
    //    }
    //}

    //public class GeminiMonsterFactory //: MonsterFactory
    //{
    //    public static IMonsterCard CreateGeminiMonster(string name)
    //    {
    //        return new GeminiMonster(name);
    //    }
    //}

    //public class RitualMonsterFactory //: MonsterFactory
    //{
    //    public static IMonsterCard CreateRitualMonster(string name)
    //    {
    //        return new RitualMonster(name);
    //    }
    //}

    //public class FusionMonsterFactory //: MonsterFactory
    //{
    //    public static IFusionMonster CreateFusionMonster(string name)
    //    {
    //        return new FusionMonster(name);
    //    }
    //}

    //public static class EffectMonsterFactory
    //{
    //    public static IMonsterCard CreateEffectMonster(string name)
    //    {
    //        return new EffectMonster(name);
    //    }
    //}

}
