namespace CardGamesAPI.Yugioh
{
    public abstract class FusionMonsterBase : MonsterCardBase, IFusionMonster
    {
        public IYugiohCard[] FusionMaterials { get; set; }

        public int Level { get; set; }

        public FusionMonsterBase()
            : this(typeof(FusionMonsterBase).Name) { }

        public FusionMonsterBase(string name)
            : this(name, YugiohMonsterCardType.Fusion) { }

        public FusionMonsterBase(string name, YugiohMonsterCardType type)
            : base(name, type) { }
    }

}