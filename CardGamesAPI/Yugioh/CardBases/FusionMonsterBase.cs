using System.Collections.Generic;

namespace CardGamesAPI.Yugioh
{
    public abstract class FusionMonsterBase : MonsterCardBase, IFusionMonster
    {
        public List<IYugiohCard> FusionMaterials { get; set; } = new List<IYugiohCard>();

        public int Level { get; set; }

        public FusionMonsterBase()
            : this(typeof(FusionMonsterBase).Name) { }

        public FusionMonsterBase(string name)
            : this(name, YugiohMonsterCardType.Fusion) { }

        public FusionMonsterBase(string name, YugiohMonsterCardType type)
            : base(name, type) { }
    }

}