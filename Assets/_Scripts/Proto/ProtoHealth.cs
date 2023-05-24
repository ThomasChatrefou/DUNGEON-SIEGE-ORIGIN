using NaughtyAttributes;
using UnityEngine;

// [TODO] move this in another script
public interface ICharacterHealth
{
    public void TakeDamage(int amount);
}

public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private bool _raiseDeathEvent;
    [ShowIf("_raiseDeathEvent")]
    [SerializeField] private ProtoCharacterDiedEventChannelSO _characterDiedEventChannel;
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_raiseDeathEvent)
        {
            _characterDiedEventChannel.RaiseEvent();
        }
        Destroy(gameObject);
    }
}
