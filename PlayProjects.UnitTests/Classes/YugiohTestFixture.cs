using CardGamesAPI.Yugioh;
using System.Collections.Generic;
using static CardGamesAPI.Yugioh.YugiohCardFactory;

namespace PlayProjects.UnitTests
{
    public static class YugiohTestFixture
    {
        public static List<IYugiohCard> CreateCards()
        {
            //todo: create a random card type (not just monster)!
            return new List<IYugiohCard> {
                CreateBasicYugiohCard<MonsterCard>(),
                CreateBasicYugiohCard<MonsterCard>(),
                CreateBasicYugiohCard<MonsterCard>(),
                CreateBasicYugiohCard<MonsterCard>(),
                CreateBasicYugiohCard<MonsterCard>(),
                CreateBasicYugiohCard<MonsterCard>(),
            };
        }
    }
}
