using System.Diagnostics;

namespace CardGamesAPI.Yugioh.Implementors
{
    public abstract partial class TrapCardBase
    {
        protected TrapImplementation implementation;
        public TrapImplementation TrapImplementation { set { implementation = value; } }

        public virtual void Activate()
        {
            implementation.ActivateEffect();
        }
    }

    /// <summary>
    /// Implements Trap 'bridge'
    /// </summary>
    public abstract class TrapImplementation
    {
        public abstract void ActivateEffect();
    }

    class ContinuousTrapEffectImplementation : TrapImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("Effect Activated!  [Wall of Revealing light] Paid 3000 LP, opponent monsters below 3000 cannot attack!");
            //throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

    class TrapMonsterEffectImplementation : TrapImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("Effect Activated!  [Metal Reflect Slime] Summoned... ATK or DEF mode?");
            //throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

    class CounterTrapEffectImplementation : TrapImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("Effect Activated!  [Solemn Judgement] Paid half and negated monster summon!");
            //throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

}
