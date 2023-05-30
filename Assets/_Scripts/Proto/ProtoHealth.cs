using NaughtyAttributes;
using UnityEngine;

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
        if (!isActiveAndEnabled)
        {
            return;
        }

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
