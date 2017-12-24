﻿namespace CardGamesAPI.Yugioh
{
    //Source: http://yugioh.wikia.com/wiki/Synchro_Monster
    public interface ISynchroMonster
    {
        IYugiohCard[] SynchroMaterials { get; set; }
        ICardEffect CardEffect { get; set; }
        int Level { get; set; }

    }
}