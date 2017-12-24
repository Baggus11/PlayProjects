using CardGamesAPI.Yugioh.Classes;
using System.Collections;

namespace CardGamesAPI.Yugioh.Builders
{
    internal class SynchroMonsterBuilder : MonsterCardBuilderBase
    {
        public SynchroMonsterBuilder() : base(YugiohMonsterCardType.Synchro) { }

        public void AddNonTunerSynchroMaterials(int materialsRequired)
        {
            for (int i = 0; i < materialsRequired; i++)
            {
                ((_monster as ISynchroMonster).SynchroMaterials as IList).Add(new MonsterCard());
            }
        }

        public void AddTunerSynchroMaterial(int materialsRequired)
        {
            ((_monster as ISynchroMonster).SynchroMaterials as IList).Add(new TunerMonster());
        }
    }
}
