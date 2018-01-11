using CardGamesAPI.Yugioh.Classes;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CardGamesAPI.Yugioh.Builders
{
    public static partial class YugiohCardDirector
    {
        //static IYugiohCardBuilder builder = null;
        static IYugiohCard _card = null;
        public static IYugiohCard BuildCard(object details)
        {
            try
            {
                var cardTypes = details.GetType().GetProperties()
                    .Where(property => property.PropertyType.IsEnum)
                    .ToDictionary(property => property.PropertyType, property => property.GetValue(details));

                var cardMainType = (YugiohCardType)cardTypes[typeof(YugiohCardType)];

                switch (cardMainType)
                {
                    case YugiohCardType.Monster:
                        _card = templates[cardMainType][(YugiohMonsterCardType)cardTypes[typeof(YugiohMonsterCardType)]];
                        break;
                    case YugiohCardType.Spell:
                        _card = templates[cardMainType][(YugiohSpellCardType)cardTypes[typeof(YugiohSpellCardType)]];
                        break;
                    case YugiohCardType.Trap:
                        _card = templates[cardMainType][(YugiohTrapCardType)cardTypes[typeof(YugiohTrapCardType)]];
                        break;
                    case YugiohCardType.Token:
                        _card = new Token();
                        break;
                    default:
                        break;
                }

                _card.Slurp(details);
                return _card;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString()));
                throw;
            }
        }

        private static Dictionary<YugiohCardType, Dictionary<Enum, IYugiohCard>> templates =
                new Dictionary<YugiohCardType, Dictionary<Enum, IYugiohCard>>()
                {
                    [YugiohCardType.Monster] = new Dictionary<Enum, IYugiohCard>()
                    {
                        [YugiohMonsterCardType.Normal] = new NormalMonster(),
                        [YugiohMonsterCardType.Effect] = new EffectMonster(),
                        [YugiohMonsterCardType.FlipEffect] = new FlipEffectMonster(),
                        [YugiohMonsterCardType.Fusion] = new FusionMonster(),
                        [YugiohMonsterCardType.Synchro] = new SynchroMonster(),
                        [YugiohMonsterCardType.XYZ] = new XYZMonster(),
                        [YugiohMonsterCardType.XYZPendulum] = new XYZPendulumMonster(),
                        [YugiohMonsterCardType.Gemini] = new GeminiMonster(),
                        [YugiohMonsterCardType.FusionPendulum] = new FusionPendulumMonster(),
                        [YugiohMonsterCardType.NormalPendulum] = new NormalPendulumMonster(),
                        [YugiohMonsterCardType.Pendulum] = new PendulumMonster(),
                        [YugiohMonsterCardType.Ritual] = new RitualMonster(),
                        [YugiohMonsterCardType.TunerSynchro] = new TunerSynchroMonster(),
                        [YugiohMonsterCardType.SynchroPendulum] = new SynchroPendulumMonster(),
                        [YugiohMonsterCardType.Tuner] = new TunerMonster(),
                        [YugiohMonsterCardType.Token] = new Token(),
                    },

                    [YugiohCardType.Spell] = new Dictionary<Enum, IYugiohCard>()
                    {
                        [YugiohSpellCardType.Continuous] = new ContinuousSpell(),
                        [YugiohSpellCardType.Equip] = new EquipSpell(),
                        [YugiohSpellCardType.Field] = new FieldSpell(),
                        [YugiohSpellCardType.Normal] = new NormalSpell(),
                        [YugiohSpellCardType.QuickPlay] = new QuickPlaySpell(),
                        [YugiohSpellCardType.Ritual] = new RitualSpell(),
                    },

                    [YugiohCardType.Trap] = new Dictionary<Enum, IYugiohCard>()
                    {
                        [YugiohTrapCardType.Normal] = new NormalTrap(),
                        [YugiohTrapCardType.Continuous] = new ContinuousTrap(),
                        [YugiohTrapCardType.Counter] = new CounterTrap(),
                        [YugiohTrapCardType.TrapMonster] = new TrapMonster(),
                    },
                };
    }
}