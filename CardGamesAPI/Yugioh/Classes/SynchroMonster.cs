namespace CardGamesAPI.Yugioh.Classes
{
    public class SynchroMonster : SynchroMonsterBase, ISynchroMonster
    {
        public SynchroMonster()
            : this(typeof(SynchroMonster).Name)
        { }

        public SynchroMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Synchro;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}