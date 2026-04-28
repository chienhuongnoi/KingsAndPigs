using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    // Khai báo các sự kiện
    public event Action OnTakeDamage;
    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // Nếu máu <= 0, phát sự kiện OnDeath
            OnDeath?.Invoke();
        }
        else
        {
            // Nếu vẫn còn sống, phát sự kiện OnTakeDamage
            OnTakeDamage?.Invoke();
        }
        Debug.Log("Current health: " + currentHealth);
    }
}