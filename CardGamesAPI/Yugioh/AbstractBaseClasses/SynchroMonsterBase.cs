namespace CardGamesAPI.Yugioh
{
    public class SynchroMonsterBase : MonsterCardBase, ISynchroMonster
    {
        public IYugiohCard[] SynchroMaterials { get; set; }
        public int Level { get; set; }

        public SynchroMonsterBase(string name)
        {
            CardName = name;
        }

        public override void Dispose()
        {
            SynchroMaterials = null;
            base.Dispose();
        }

    }

}