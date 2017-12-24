using System.Collections;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class XYZMonsterBuilder : MonsterCardBuilderBase
    {
        public XYZMonsterBuilder() : base(YugiohMonsterCardType.XYZ)
        {
        }

        public void AddXYZMaterials(IMonsterCard material) => ((_monster as IXYZMonster).XYZMaterials as IList).Add(material);

    }
}
