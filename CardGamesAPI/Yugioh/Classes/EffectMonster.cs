namespace CardGamesAPI.Yugioh
{
    public class EffectMonster : MonsterCardBase, IEffectMonster
    {
        public int SpellSpeed { get; set; }

        public EffectMonster()
            : this(typeof(EffectMonster).Name)
        { }

        public EffectMonster(string monsterName)
            : this(monsterName, YugiohMonsterCardType.Effect) { }

        public EffectMonster(string monsterName, YugiohMonsterCardType monsterCardType)
            : base(monsterName, monsterCardType) { }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}
