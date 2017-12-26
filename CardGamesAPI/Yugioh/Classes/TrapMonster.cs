namespace CardGamesAPI.Yugioh.Classes
{
    internal class TrapMonster : MonsterCardBase, ITrapCard
    {
        public YugiohTrapCardType TrapType { get; set; }

        public TrapMonster()
        {
        }

    }
}