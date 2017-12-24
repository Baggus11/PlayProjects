namespace CardGamesAPI.Yugioh.Classes
{
    public class GeminiMonster : MonsterCardBase, IEffectMonster, INormalMonster
    {
        public int Level { get; set; }

        public GeminiMonster()
            : this(typeof(GeminiMonster).Name)
        { }

        public GeminiMonster(string name)
            : base(name)
        {
            MonsterCardType = YugiohMonsterCardType.Gemini;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }

}