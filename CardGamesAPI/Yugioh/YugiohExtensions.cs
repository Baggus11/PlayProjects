using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardGamesAPI.Yugioh
{
    public static class YugiohExtensions
    {
        public static void Slurp<T>(this T card, object source)
            where T : class, IYugiohCard
        {
            var sourceProperties = source.GetType().GetProperties();

            Type destinationType = card.GetType();
            Type sourceType = source.GetType();

            var mappableProperties = from sourceProperty in sourceProperties
                                     let targetProperty = destinationType.GetProperty(sourceProperty.Name)
                                     where sourceProperty.CanRead
                                     && targetProperty != null
                                     && (targetProperty.GetSetMethod(true) != null
                                     && !targetProperty.GetSetMethod(true).IsPrivate)
                                     && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                                     && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)
                                     select new { sourceProperty = sourceProperty, targetProperty = targetProperty };

            foreach (var property in mappableProperties)
            {
                property.targetProperty.SetValue(card, property.sourceProperty.GetValue(source, null), null);
            }

        }

        ////Refactor for use in abstract base classes:
        //public static void Map<T, R>(ref T source, ref R destination)
        //    where T : new()
        //    where R : new()
        //{
        //    if (source == null) source = new T();
        //    if (destination == null) destination = new R();

        //    Type typeDestination = destination.GetType();
        //    Type typeOfSource = source.GetType();

        //    var mappableProperties = from sourceProperty in typeOfSource.GetProperties()
        //                             let targetProperty = typeDestination.GetProperty(sourceProperty.Name)
        //                             where sourceProperty.CanRead
        //                             && targetProperty != null
        //                             && (targetProperty.GetSetMethod(true) != null
        //                             && !targetProperty.GetSetMethod(true).IsPrivate)
        //                             && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
        //                             && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)
        //                             select new { sourceProperty = sourceProperty, targetProperty = targetProperty };

        //    foreach (var property in mappableProperties)
        //    {
        //        property.targetProperty.SetValue(destination, property.sourceProperty.GetValue(source, null), null);
        //    }

        //}

        public static T Clone<T>(this object item)
        {
            if (item != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();

                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);

                T result = (T)formatter.Deserialize(stream);

                stream.Close();

                return result;
            }

            return default(T);
        }

        public static void CopyProperties(this object source, object destination)
        {
            var destinationProperties = destination?.GetType().GetProperties();
            var sourceProperties = source?.GetType().GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                foreach (var destProperty in destinationProperties)
                {
                    if (destProperty.Name == sourceProp.Name && destProperty.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProperty.SetValue(destinationProperties, sourceProp.GetValue(
                            sourceProp, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }

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
