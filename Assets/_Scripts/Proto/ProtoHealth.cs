using NaughtyAttributes;
using UnityEngine;

// [TODO] move this in another script
public interface ICharacterHealth
{
    public void TakeDamage(int amount);
}


public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private int _maxHealth = 3;

    [Space(10)]
    [SerializeField] private bool _isDeathNotified;
    [ShowIf("_isDeathNotified")]
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _deathChannel;

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

    [Button]
    private void Die()
    {
        if (_isDeathNotified)
        {
            _deathChannel.RequestRaiseEvent();
        }
        Destroy(gameObject);
    }
}
