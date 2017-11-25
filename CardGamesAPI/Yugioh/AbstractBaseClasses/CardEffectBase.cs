namespace CardGamesAPI.Yugioh
{
    public abstract class CardEffectBase : ICardEffect
    {
        public string EffectText { get; set; } = "";
        public string EffectType { get; set; } = "";
        public string PreConditions { get; set; }
        public string PostConditions { get; set; }
        public abstract YugiohMove Activate(YugiohMove currentState);

    }
}
