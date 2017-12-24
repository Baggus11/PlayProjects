namespace CardGamesAPI.Yugioh
{
    //Source: http://yugioh.wikia.com/wiki/Ritual_Monster
    public interface IRitualMonster : IMonsterCard
    {
        ISpellCard RitualSpell { get; set; }
        IYugiohCard[] RitualMaterials { get; set; }
        ICardEffect CardEffect { get; set; }
        int Level { get; set; }
    }
}
