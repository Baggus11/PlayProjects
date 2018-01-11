using CardGamesAPI.Yugioh.Classes;
using System.Collections;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class XYZMonsterBuilder : MonsterCardBuilderBase
    {
        public XYZMonsterBuilder() : base(YugiohMonsterCardType.XYZ) { }

        public void AddXYZMaterials(IMonsterCard material) => ((_card as IXYZMonster).XYZMaterials as IList).Add(material);

        public override void Build(object details)
        {
            XYZMonster derived = new XYZMonster();
            derived.Slurp(details);
            _card = derived;
        }
    }
}
