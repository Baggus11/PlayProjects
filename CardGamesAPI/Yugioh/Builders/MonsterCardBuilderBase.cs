using CardGamesAPI.Yugioh.Factories;
using CardGamesAPI.Yugioh.Interfaces;
using System;
using System.Linq;

namespace CardGamesAPI.Yugioh.Builders
{
    internal abstract class MonsterCardBuilderBase : IYugiohCardBuilder
    {
        protected IYugiohCard _monster;

        public MonsterCardBuilderBase(YugiohMonsterCardType monsterCardType)
        {
            _monster = YugiohCardFactory.CreateCard(monsterCardType.GetBaseType()) as IYugiohCard;
        }

        public virtual void SetAttack(int attack) => ((IMonsterCard)_monster).Attack = attack;

        public virtual void SetDefense(int defense) => ((IMonsterCard)_monster).Defense = defense;

        public virtual void Build(object[] details)
        {
            var stats = details.OfType<MonsterStats>().FirstOrDefault();

            SetAttack(stats.Attack);
            SetDefense(stats.Defense);
        }

        private void SetDefense(object defense)
        {
            throw new NotImplementedException();
        }



        public IYugiohCard Card => _monster;
    }

}
