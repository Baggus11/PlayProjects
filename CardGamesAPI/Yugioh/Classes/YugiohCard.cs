using System;

namespace CardGamesAPI.Yugioh.Classes
{
    /// <summary>
    /// A Template Card
    /// Usage: cloning or prototyping
    /// Why this exists: To have a default prototype for all Yugioh cards
    ///     that DOES NOT implement YugiohCard base abstract class(es).
    /// </summary>
    public class YugiohCard : IYugiohCard
    {
        public Guid SysGuid => Guid.Empty;
        public int SpellSpeed { get; set; }
        public string KonamiID { get; set; }
        public string CardName { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string LocalPath { get; set; }
        public YugiohCardType CardType { get; set; }
        public YugiohCardPosition Position { get; set; }

        public event EventHandler<YugiohCardEventArgs> EffectTriggered;

        public object Clone() => MemberwiseClone();

        public void Dispose() { }
    }
}
