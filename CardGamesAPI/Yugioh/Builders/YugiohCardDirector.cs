using CardGamesAPI.Yugioh.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGamesAPI.Yugioh.Builders
{
    public static class YugiohCardDirector
    {
        private static Dictionary<Type, IYugiohCardBuilder> builds = new Dictionary<Type, IYugiohCardBuilder>()
        {
            [typeof(IFusionMonster)] = new FusionMonsterBuilder(),
        };

        public static IYugiohCard BuildCard(object details)
        {
            var properties = details.GetType().GetProperties();

            var lookup = details.GetType().GetProperties()
                .ToLookup(property => property.PropertyType, property => property.GetValue(details));

            lookup.Dump();
            //YugiohCardType cardBaseType = properties
            //    .Single(property => 
            //    property.PropertyType.Equals(typeof(YugiohCardType)))
            //    ;
            //var card = YugiohCardFactory.CreateCard(cardBaseType);


            var builder = new FusionMonsterBuilder();//ex

            //kvps.Dump();

            //kvps.OfType<int>().First().Dump();
            //int attack = kvps.OfType<int>().First();
            //int attack = details.OfType<int>().First();
            //var cardBaseType = details.OfType<YugiohCardType>().First();

            //var materials = details as IEnumerable IYugiohCard;

            //get the appropriate builder abstract class
            //this class is the Director

            //map properties of details to those of the prototype monster/spell/trap.

            //return builds[typeof(T)].Build(details);

            return builder.Card;
            //throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);

        }

    }
}
