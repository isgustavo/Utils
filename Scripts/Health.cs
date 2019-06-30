using System;
using UnityEngine;

namespace odt.util
{
    [Serializable]
    public class Health
    {
        [SerializeField]
        private int InitHealth;
        public int CurrentHealth { get; private set; }

        public Action<int> OnHealthChanged = delegate { };

        public void Init()
        {
            CurrentHealth = InitHealth;
        }

        public void ChangeHealth(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, InitHealth);
            OnHealthChanged.Invoke(CurrentHealth);
        }
    }
}

