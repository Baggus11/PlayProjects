namespace CardGamesAPI.Yugioh
{
    public abstract class SpellCardBase : YugiohCardBase, ISpellCard
    {
        public YugiohSpellType SpellType { get; set; }
    }
}
