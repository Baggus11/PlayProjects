using System.Collections;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class FusionMonsterBuilder : MonsterCardBuilderBase
    {
        public FusionMonsterBuilder() : base(YugiohMonsterCardType.Fusion) { }

        public void build(object[] details) => Build(details);

        public void AddFusionMaterial(IYugiohCard material) => ((_monster as IFusionMonster).FusionMaterials as IList).Add(material);

    }
}
