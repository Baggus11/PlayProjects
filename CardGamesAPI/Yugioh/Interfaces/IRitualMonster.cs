namespace CardGamesAPI.Yugioh
{
    //Source: http://yugioh.wikia.com/wiki/Ritual_Monster
    public interface IRitualMonster : IMonsterCard
    {
        ISpellCard RitualSpell { get; set; }
        //ritual materials?
        ICardEffect CardEffect { get; set; }
        int Level { get; set; }

    }

}
