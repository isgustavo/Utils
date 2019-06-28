using UnityEngine;

namespace odt.util
{
    public abstract class Ability
    {
        protected float cooldown;
        protected float lastUseTime;

        protected Ability(float cooldown)
        {
            this.cooldown = cooldown;
        }

        public abstract void Use();

        protected bool CanUse()
        {
            return Time.time - lastUseTime > cooldown;
        }

        public float GetCooldown()
        {
            float c = cooldown - (Time.time - lastUseTime);
            return (c <= 0) ? 0 : c;
        }

    }
}
