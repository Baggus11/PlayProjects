using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public interface IFusionMonster : IMonsterCard
    {
        List<IYugiohCard> FusionMaterials { get; set; }
        ICardEffect CardEffect { get; set; }
        int Level { get; set; }
    }
}
