using System;
using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public static class YugiohExtensions
    {
        public static YugiohCardType GetBaseType<TEnum>(this TEnum cardType)
           where TEnum : struct, IConvertible, IFormattable, IComparable
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            var @switch = new Dictionary<Enum, YugiohCardType>
            {
                [YugiohMonsterCardType.Fusion] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Synchro] = YugiohCardType.Monster,
                [YugiohMonsterCardType.XYZ] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Ritual] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Tuner] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Effect] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Normal] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Gemini] = YugiohCardType.Monster,
                [YugiohMonsterCardType.FlipEffect] = YugiohCardType.Monster,
                [YugiohMonsterCardType.Pendulum] = YugiohCardType.Monster,
                [YugiohMonsterCardType.TunerSynchro] = YugiohCardType.Monster,
                [YugiohMonsterCardType.NormalPendulum] = YugiohCardType.Monster,
                [YugiohMonsterCardType.FusionPendulum] = YugiohCardType.Monster,
                [YugiohMonsterCardType.SynchroPendulum] = YugiohCardType.Monster,
                [YugiohMonsterCardType.XYZPendulum] = YugiohCardType.Monster,

                [YugiohSpellCardType.QuickPlay] = YugiohCardType.Spell,
                [YugiohSpellCardType.Continuous] = YugiohCardType.Spell,
                [YugiohSpellCardType.Equip] = YugiohCardType.Spell,
                [YugiohSpellCardType.Field] = YugiohCardType.Spell,
                [YugiohSpellCardType.Normal] = YugiohCardType.Spell,
                [YugiohSpellCardType.Ritual] = YugiohCardType.Spell,

                [YugiohTrapCardType.Continuous] = YugiohCardType.Trap,
                [YugiohTrapCardType.Counter] = YugiohCardType.Trap,
                [YugiohTrapCardType.Normal] = YugiohCardType.Trap,
                [YugiohTrapCardType.TrapMonster] = YugiohCardType.Trap, //Todo: This one will be ultra weird!
            };

            return @switch[cardType as Enum];
        }
    }
}
