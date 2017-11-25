namespace CardGamesAPI.Yugioh
{
    public abstract class TunerMonsterBase : MonsterCardBase, ITunerMonster
    {
        public int Level { get; set; }

        public TunerMonsterBase(string name)
        {
            CardName = name;
        }

    }

}
