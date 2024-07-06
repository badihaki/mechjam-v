using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int maxHealth { get; private set; }
    [field: SerializeField] public int currentHealth { get; private set; }

    public delegate void HealthChange(int health);
    public event HealthChange onHealthChange;

    public void InitializeHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        onHealthChange?.Invoke(currentHealth);
    }

    public void ChangeHealth(int health)
    {
        currentHealth = health;
        onHealthChange?.Invoke(currentHealth);
    }
}
