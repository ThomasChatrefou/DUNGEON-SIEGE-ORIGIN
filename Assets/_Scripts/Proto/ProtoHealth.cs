using NaughtyAttributes;
using System;
using UnityEngine;

public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private int _maxHealth = 3;

    [Space(10)]
    [SerializeField] private bool _isDeathNotified;
    [SerializeField] private bool _isHitNotified;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isDeathNotified")]
    [SerializeField] private VoidEventChannelSO _deathChannel;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isHitNotified")]
    [SerializeField] private VoidEventChannelSO _hitChannel;

    //GREYBOX TO REMOVE
    public event Action _onHitEvent;

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
        if (_isHitNotified)
        {
            _hitChannel.RequestRaiseEvent();
        }
        if (_onHitEvent != null)
        {
            _onHitEvent.Invoke();
        }

        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    [Button]
    public void Die()
    {
        if (_isDeathNotified)
        {
            _deathChannel.RequestRaiseEvent();
        }
        Destroy(gameObject);
    }
}
