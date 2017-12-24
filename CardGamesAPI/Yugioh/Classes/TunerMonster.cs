namespace CardGamesAPI.Yugioh.Classes
{
    //Source: http://yugioh.wikia.com/wiki/Tuner_monster
    public class TunerMonster : TunerMonsterBase, ITunerMonster
    {
        public TunerMonster()
            : this(typeof(TunerMonster).Name)
        { }

        public TunerMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Tuner;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}