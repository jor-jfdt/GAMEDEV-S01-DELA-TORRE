using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameManager gameManager;

    void Start()
    {
        ResetHealth();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Player hit! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            gameManager.ResetGame();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Prevent health from going over the maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log($"Bonus acquired! Health restored to: {currentHealth}");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
