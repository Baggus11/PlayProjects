namespace CardGamesAPI.Yugioh
{
    //Source: http://yugioh.wikia.com/wiki/Pendulum_Monster
    public interface IPendulumMonster : IMonsterCard
    {
        int LeftScale { get; set; }
        int RightScale { get; set; }
        ICardEffect PendulumEffect { get; set; }
        ICardEffect CardEffect { get; set; }

    }

}