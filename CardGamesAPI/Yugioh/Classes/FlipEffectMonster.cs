namespace CardGamesAPI.Yugioh.Classes
{
    internal class FlipEffectMonster : MonsterCardBase, IFlipEffectMonster
    {
        public FlipEffectMonster()
            : this(typeof(FlipEffectMonster).Name)
        { }

        public FlipEffectMonster(string monsterName)
            : this(monsterName, YugiohMonsterCardType.FlipEffect) { }

        public FlipEffectMonster(string monsterName, YugiohMonsterCardType monsterCardType)
            : base(monsterName, monsterCardType) { }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}