using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int MaxHealth { get; private set; }
    private int CurrentHealth { get; set; }

    public Action<int> OnHealthChanged = delegate { };

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ChangeHealth(int amount)
    {
        CurrentHealth += Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        OnHealthChanged.Invoke(CurrentHealth);
    }
}
