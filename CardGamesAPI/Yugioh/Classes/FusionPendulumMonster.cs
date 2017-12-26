using System.Collections.Generic;

namespace CardGamesAPI.Yugioh.Classes
{
    public class FusionPendulumMonster : PendulumMonsterBase, IFusionMonster
    {
        public List<IYugiohCard> FusionMaterials { get; set; }
        //public List<IYugiohCard> FusionMaterials { get; set; }
        public int Level { get; set; }

        public FusionPendulumMonster()
            : this(typeof(FusionPendulumMonster).Name) { }

        public FusionPendulumMonster(string name)
            : this(name, YugiohMonsterCardType.FusionPendulum) { }

        public FusionPendulumMonster(string name, YugiohMonsterCardType monsterCardType)
            : base(name, monsterCardType) { }

        public override void Dispose()
        {
            FusionMaterials = null;
            base.Dispose();
        }

    }

}