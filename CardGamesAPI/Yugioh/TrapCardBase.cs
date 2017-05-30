namespace CardGamesAPI.Yugioh
{
    public abstract class TrapCardBase : YugiohCardBase, ITrapCard
    {
        public YugiohTrapType TrapType { get; set; }
    }
}
