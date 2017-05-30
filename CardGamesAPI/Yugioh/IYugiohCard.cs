using System;
namespace CardGamesAPI.Yugioh
{
    public interface IYugiohCard
    {
        Guid SysGuid { get; }
        string KonamiID { get; set; }
        string CardName { get; set; }
        int SpellSpeed { get; set; } //for effects, obv not every card has one
        YugiohCardBaseType CardBaseType { get; set; }
        YugiohCardPosition Position { get; set; }
        event EventHandler<YugiohCardEventArgs> EffectTriggered;
        //void ChangePosition(IYugiohCard card, YugiohCardPosition position);
    }
}
