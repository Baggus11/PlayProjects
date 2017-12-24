using System;
using System.Diagnostics;
using System.Reflection;

namespace CardGamesAPI.Yugioh
{

    public abstract partial class SpellCardBase
    {
        protected SpellImplementation implementation;
        public SpellImplementation SpellImplementation { set { implementation = value; } }

        public virtual void Activate()
        {
            implementation.ActivateEffect();
        }
    }

    /// <summary>
    /// Implements Spell 'bridges'
    /// </summary>
    public abstract class SpellImplementation
    {
        public abstract void ActivateEffect();
    }

    class EquipSpellEffectImplementation : SpellImplementation
    {
        IMonsterCard _equipTarget;

        public override void ActivateEffect()
        {
            Debug.WriteLine("Effect Activated! Monster equipped with ");
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

    class FieldSpellEffectImplementation : SpellImplementation
    {
        public override void ActivateEffect()
        {
            Debug.WriteLine("Field spell 'Sogen' activated!");
            throw new NotImplementedException(MethodBase.GetCurrentMethod().Name);
        }
    }

}
