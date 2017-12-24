using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CardGamesAPI.Yugioh
{
    public abstract class CardEffect<TCard> : ICardEffect
    {
        public string EffectText { get; set; } = "";
        public string PreConditions { get; set; }
        public string PostConditions { get; set; }

        protected CardEffect()
        {
            //stub
        }

        public bool TryExecute(ref IEnumerable<TCard> collection, Action<TCard> action)
        {
            try
            {
                var cached = collection;

                foreach (var item in cached ?? Enumerable.Empty<TCard>())
                {
                    action(item);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString()));
                //_logger.LogError(ex);
                return false;
                throw;
            }
        }

        private static bool TryExecute(ref TCard item, Action<TCard> action)
        {
            try
            {
                action(item);
            }
            catch (Exception)
            {
                throw;
            }

            return true;

        }
    }
}
