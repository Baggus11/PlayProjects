using CardGamesAPI.Yugioh.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CardGamesAPI.Yugioh.Builders
{
    public static partial class YugiohCardDirector
    {
        static IYugiohCardBuilder builder = null;

        public static IYugiohCard BuildCard(object details)
        {
            var cardTypes = details.GetType()
                .GetProperties()
                .Where(property => property.PropertyType.IsEnum)
                .ToDictionary(property => property.PropertyType, property => property.GetValue(details));

            YugiohCardType cardType = (YugiohCardType)cardTypes[typeof(YugiohCardType)];

            switch (cardType)
            {
                case YugiohCardType.Monster:
                    builder = Builders[cardType][(YugiohMonsterCardType)cardTypes[typeof(YugiohMonsterCardType)]];
                    break;
                case YugiohCardType.Spell:
                    builder = Builders[cardType][(YugiohSpellCardType)cardTypes[typeof(YugiohSpellCardType)]];
                    break;
                case YugiohCardType.Trap:
                    builder = Builders[cardType][(YugiohTrapCardType)cardTypes[typeof(YugiohTrapCardType)]];
                    break;
                case YugiohCardType.Token:
                    builder = new TokenBuilder();
                    break;
                default:
                    break;
            }

            Debug.WriteLine($"Using {builder.GetType().Name} Builder");
            builder?.Build(details);

            return builder?.Card;
        }

        private static Dictionary<YugiohCardType, Dictionary<Enum, IYugiohCardBuilder>> Builders =
                new Dictionary<YugiohCardType, Dictionary<Enum, IYugiohCardBuilder>>()
                {
                    [YugiohCardType.Monster] = new Dictionary<Enum, IYugiohCardBuilder>()
                    {
                        [YugiohMonsterCardType.Normal] = new NormalMonsterBuilder(),
                        [YugiohMonsterCardType.FlipEffect] = new FlipEffectMonsterBuilder(),
                        [YugiohMonsterCardType.Fusion] = new FusionMonsterBuilder(),
                        [YugiohMonsterCardType.Synchro] = new SynchroMonsterBuilder(),
                        [YugiohMonsterCardType.XYZ] = new XYZMonsterBuilder(),
                        [YugiohMonsterCardType.XYZPendulum] = new XYZPendulumMonsterBuilder(),
                        [YugiohMonsterCardType.Gemini] = new GeminiMonsterBuilder(),
                        [YugiohMonsterCardType.FusionPendulum] = new FusionPendulumMonsterBuilder(),
                        [YugiohMonsterCardType.NormalPendulum] = new NormalPendulumMonsterBuilder(),
                        [YugiohMonsterCardType.Pendulum] = new PendulumMonsterBuilder(),
                        [YugiohMonsterCardType.Ritual] = new RitualMonsterBuilder(),
                        [YugiohMonsterCardType.TunerSynchro] = new TunerSynhroMonsterBuilder(),
                        [YugiohMonsterCardType.SynchroPendulum] = new SynchroPendulumMonsterBuilder(),
                        [YugiohMonsterCardType.Tuner] = new TunerMonsterBuilder(),
                    },

                    [YugiohCardType.Spell] = new Dictionary<Enum, IYugiohCardBuilder>()
                    {
                        [YugiohSpellCardType.Continuous] = new ContinuousSpellBuilder(),
                        [YugiohSpellCardType.Equip] = new EquipSpellBuilder(),
                        [YugiohSpellCardType.Field] = new FieldSpellBuilder(),
                        [YugiohSpellCardType.Normal] = new NormalSpellBuilder(),
                        [YugiohSpellCardType.QuickPlay] = new QuickPlaySpellBuilder(),
                        [YugiohSpellCardType.Ritual] = new RitualSpellBuilder(),
                    },

                    [YugiohCardType.Trap] = new Dictionary<Enum, IYugiohCardBuilder>()
                    {
                        [YugiohTrapCardType.Continuous] = new ContinuousTrapBuilder(),
                        [YugiohTrapCardType.Counter] = new CounterTrapBuilder(),
                        [YugiohTrapCardType.Normal] = new NormalTrapBuilder(),
                        [YugiohTrapCardType.TrapMonster] = new TrapMonsterBuilder(),
                    },
                };
    }
}