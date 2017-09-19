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
        Token,
        MonsterCard,
        SpellCard,
        TrapCard
    }
    public enum YugiohMonsterType
    {
        Aqua,
        Beast,
        BeastWarrior,
        Cyberse,
        CyberseLink,
        Dinosaur,
        Dragon,
        Fairy,
        Fiend,
        Fish,
        Machine,
        Plant,
        Psychic,
        Pyro,
        Reptile,
        Rock,
        SeaSerpent,
        Spellcaster,
        Thunder,
        Warrior,
        WingedBeast,
        Wyrm,
        Zombie,
        None,
    }
    public enum YugiohMonsterAttribute
    {
        None,
        Light,
        Dark,
        Water,
        Fire,
        Wind,
        Earth,
    }
    public enum YugiohMonsterBaseType
    {
        None,
        Normal,
        Effect,
    }
    public enum YugiohTrapType
    {
        //None,
        Normal,
        Continuous,
        Counter,
        TrapMonster,
    }
    public enum YugiohSpellType
    {
        //None,
        Field,
        Equip,
        Continuous,
        Ritual,
        QuickPlay,
        //Speed,
        Pendulum,
        Normal,
    }
}
