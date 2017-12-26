namespace CardGamesAPI.Yugioh
{
    //Source: http://yugioh.wikia.com/wiki/Tuner_monster
    public interface ITunerMonster : IMonsterCard
    {
        int Level { get; set; }
    }
}
