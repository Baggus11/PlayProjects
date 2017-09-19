namespace CardGamesAPI.Yugioh
{
    public abstract class CardEffectBase : ICardEffect
    {
        public string Text { get; set; } = "";
        public string Type { get; set; } = "";

        public string PreConditions { get; set; } = "";
        public string PostConditions { get; set; } = "";

        public abstract YugiohMove Activate(YugiohMove currentState);

    }
}
