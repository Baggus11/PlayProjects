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
    public enum YugiohCardType
    {
        Token,
        Monster,
        Spell,
        Trap
    }
    public enum YugiohMonsterType
    {
        //None, //make nullable
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
        //None, //make nullable
        Light,
        Dark,
        Water,
        Fire,
        Wind,
        Earth,
    }
    public enum YugiohMonsterCardType
    {
        //None, //make nullable
        Normal,
        Effect,
        Fusion,
        Ritual,
        Synchro,
        Tuner,
        XYZ,
        Gemini,
        FlipEffect,
        Pendulum,
        TunerSynchro,
        NormalPendulum,
        FusionPendulum,
        SynchroPendulum,
        XYZPendulum,
        Token,
    }
    public enum YugiohTrapCardType
    {
        //None, //make nullable
        Normal,
        Continuous,
        Counter,
        TrapMonster,
    }
    public enum YugiohSpellCardType
    {
        //None, //make nullable
        Normal,
        Field,
        Equip,
        Continuous,
        Ritual,
        QuickPlay,
    }
}
