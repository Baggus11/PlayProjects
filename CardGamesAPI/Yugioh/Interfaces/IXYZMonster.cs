namespace CardGamesAPI.Yugioh
{
    public interface IXYZMonster : IMonsterCard
    {
        IYugiohCard[] XYZMaterials { get; set; }
        ICardEffect CardEffect { get; set; }
        int Rank { get; set; }

    }

}
