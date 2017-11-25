namespace CardGamesAPI.Yugioh
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

    //Yes, it's both types, but primarily a Synchro Monster with Tuner Attributes
    //Source: http://yugioh.wikia.com/wiki/Tuner_Synchro_Monster
    public class TunerSynchroMonster : SynchroMonsterBase, ITunerMonster
    {
        public TunerSynchroMonster()
            : this(typeof(TunerSynchroMonster).Name)
        { }

        public TunerSynchroMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.TunerSynchro;

        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}