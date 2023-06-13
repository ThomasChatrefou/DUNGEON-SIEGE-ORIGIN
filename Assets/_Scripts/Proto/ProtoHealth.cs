using UnityEngine;

public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}