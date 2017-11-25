namespace CardGamesAPI.Yugioh
{
    public class NormalMonster : MonsterCardBase, INormalMonster
    {
        public int Level { get; set; }

        public NormalMonster()
            : this(typeof(NormalMonster).Name) { }

        public NormalMonster(string name)
            : this(name, YugiohMonsterCardType.Normal) { }

        public NormalMonster(string monsterName, YugiohMonsterCardType monsterCardType)
            : base(monsterName, monsterCardType) { }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}