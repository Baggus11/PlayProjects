namespace CardGamesAPI.Yugioh
{
    public interface IEffectMonster : IMonsterCard
    {
        ICardEffect CardEffect { get; set; }
        void ActivateEffect();
    }
}