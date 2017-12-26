using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Builders
{
    //IGNORE
    public class CardBuilder : IYugiohCardBuilder
    {
        public IYugiohCard Card => throw new System.NotImplementedException();

        public void Build(object details)
        {
            throw new System.NotImplementedException();
        }

        protected void BuildDerived<T>(ref IYugiohCard _card) where T : class, IYugiohCard
        {
            IYugiohCard derivedMonster = default(T);


            //((YugiohCardBase)derivedMonster).Map(ref _card, ref derivedMonster);
            //FusionMonster blankFusion = new FusionMonster("Bob, the Fusion Master");
            //MonsterCard monster = ((MonsterCard)_monster);
            //Map(ref monster, ref blankFusion);
            //blankFusion.FusionMaterials.Add(new MonsterCard("rescue kitty"));
            //monster.Dump();
            //blankFusion.Dump();
            //_monster = blankFusion;

            _card = derivedMonster;
        }

    }
}