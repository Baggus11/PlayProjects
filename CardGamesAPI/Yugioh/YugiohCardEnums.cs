namespace CardGamesAPI.Yugioh
{
    /*Yugioh Card enums */
    public enum YugiohCardPosition
    {
        SetFaceDown,
        SetInAttackPosition,
        AttackMode, //face up
        DefenseMode,
    }
    public enum YugiohCardBaseType
    {
        Monster,
        Spell,
        Trap
    }
    public enum YugiohMonsterType
    {
        Dragon,
        Fairy,
        Spellcaster,
    }
    public enum YugiohMonsterAttribute
    {
        Light,
        Dark
    }
    public enum YugiohMonsterBaseType
    {
        Normal,
        Effect,
    }
    public enum YugiohTrapType
    { }
    public enum YugiohSpellType
    { }
}
