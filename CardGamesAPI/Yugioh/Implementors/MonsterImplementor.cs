using System;
using System.Diagnostics;
using System.Reflection;

namespace CardGamesAPI.Yugioh
{
    public abstract partial class MonsterCardBase
    {
        protected MonsterImplementation implementation;
        public MonsterImplementation MonsterImplementation { set { implementation = value; } }

        public virtual void ActivateEffect()
        {
            implementation.ActivateEffect();
        }
    }

    // What to do when an Effect is populated and Activate() is called:
    class MonsterWithEffectImplementation : MonsterImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("Effect Activated!  [Blade Knight] ATK +400!");
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

    // What to do when no effect is applied or is null:
    class NonEffectMonsterImplementation : MonsterImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("No Effect Could be Activated!  Nothing happened...");
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// Implements Monster 'bridge'
    /// </summary>
    public abstract class MonsterImplementation
    {
        public abstract void ActivateEffect();
    }
}
