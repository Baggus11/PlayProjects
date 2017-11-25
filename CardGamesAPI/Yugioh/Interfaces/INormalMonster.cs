namespace CardGamesAPI.Yugioh
{
    public interface INormalMonster : IMonsterCard
    {
        int Level { get; set; }

    }
}