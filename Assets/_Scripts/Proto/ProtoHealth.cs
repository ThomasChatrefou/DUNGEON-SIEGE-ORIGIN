using UnityEngine;

// [TODO] move this in another script
public interface ICharacterHealth
{
    public void TakeDamage(int amount);
}


public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public GameObject deathScreen;

    private void Start()
    {
        deathScreen.SetActive(false);
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

    private void Die()
    {
        Destroy(gameObject);
        deathScreen.SetActive(true);
    }
}
