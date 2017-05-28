using CardGamesAPI.Constants;
namespace CardGamesAPI.Yugioh
{
    public interface IMonsterCard : IYugiohCard
    {
        YugiohMonsterType MonsterBaseType { get; set; }
        YugiohMonsterAttribute MonsterAttribute { get; set; }
    }
}
