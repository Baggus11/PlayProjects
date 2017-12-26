using CardGamesAPI.Yugioh.Factories;
using CardGamesAPI.Yugioh.Interfaces;

namespace CardGamesAPI.Yugioh.Builders
{
    public abstract class MonsterCardBuilderBase : IYugiohCardBuilder
    {
        protected IMonsterCard _card;

        public MonsterCardBuilderBase(YugiohMonsterCardType monsterCardType) => _card = YugiohCardFactory.CreateCard(monsterCardType.GetBaseType()) as IMonsterCard;

        //public virtual void Build(object details) => _monster.Slurp(details);
        public abstract void Build(object details);

        public IYugiohCard Card => _card;
    }
}
