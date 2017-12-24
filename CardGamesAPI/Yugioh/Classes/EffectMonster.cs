namespace CardGamesAPI.Yugioh.Classes
{
    public class EffectMonster : MonsterCardBase, IEffectMonster
    {
        public EffectMonster()
            : this(typeof(EffectMonster).Name)
        { }

        public EffectMonster(string monsterName)
            : this(monsterName, YugiohMonsterCardType.Effect) { }

        public EffectMonster(string monsterName, YugiohMonsterCardType monsterCardType)
            : base(monsterName, monsterCardType) { }
    }

}
