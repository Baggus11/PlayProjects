namespace CardGamesAPI.Yugioh
{
    public interface IFusionMonster : IMonsterCard
    {
        IYugiohCard[] FusionMaterials { get; set; }
        //List<IYugiohCard> FusionMaterials { get; set; }
        ICardEffect CardEffect { get; set; }
        int Level { get; set; }

    }

}
