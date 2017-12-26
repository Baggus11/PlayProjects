namespace CardGamesAPI.Yugioh.Classes
{
    public class RitualMonster : RitualMonsterBase
    {
        public RitualMonster() : this(typeof(RitualMonster).Name) { }

        public RitualMonster(string name)
            : base(name) => MonsterCardType = YugiohMonsterCardType.Ritual;

        public override void Dispose() => base.Dispose();
    }
}