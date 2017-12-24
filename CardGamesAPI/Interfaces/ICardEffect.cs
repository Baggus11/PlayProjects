namespace CardGamesAPI
{
    public interface ICardEffect
    {
        string EffectText { get; }
        string PreConditions { get; set; }
        string PostConditions { get; set; }
    }
}
