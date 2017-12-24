namespace CardGamesAPI.Yugioh
{
    public abstract class RitualMonsterBase : MonsterCardBase, IRitualMonster
    {
        public ISpellCard RitualSpell { get; set; }
        public int Level { get; set; }
        public IYugiohCard[] RitualMaterials { get; set; }

        public RitualMonsterBase(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Ritual;
        }

        public override void Dispose()
        {
            RitualSpell = null;
            base.Dispose();
        }

    }

}
