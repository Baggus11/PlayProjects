namespace CardGamesAPI.Yugioh
{
    public interface IEffectMonster : IMonsterCard, IYugiohCard
    {
        ICardEffect CardEffect { get; set; }
        void ActivateEffect();
    }
}