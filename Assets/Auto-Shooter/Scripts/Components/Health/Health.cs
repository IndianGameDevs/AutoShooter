using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int testDamage;

    public UnityEvent<int, int, int> onHealthChanged;

    private int currentHealth;

    public int maxHealth;

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }

    [ContextMenu("Inflict Damage")]
    private void InflictDamage()
    {
        UpdateCurrentHealth(testDamage);
    }

    public void UpdateCurrentHealth(int change)
    {
        int previous = currentHealth;
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHealthChanged?.Invoke(previous, currentHealth, maxHealth);
    }
}
