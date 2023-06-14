using NaughtyAttributes;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterDataManager))]
public class CharacterHealth : MonoBehaviour, ICharacterHealth
{
    //GREYBOX TO REMOVE
    public event Action OnHitEvent;

    [SerializeField] private bool _isDeathNotified;
    [SerializeField] private bool _isHitNotified;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isDeathNotified")]
    [SerializeField] private VoidEventChannelSO _deathChannel;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isHitNotified")]
    [SerializeField] private VoidEventChannelSO _hitChannel;

    private CharacterDataManager _characterDataManager;
    private float _currentHealth;

    public void TakeDamage(int amount)
    {
        if (!isActiveAndEnabled) return;

        if (_isHitNotified)
        {
            _hitChannel.RequestRaiseEvent();
        }

        OnHitEvent?.Invoke();

        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
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

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _characterDataManager.Data.MaxHealth;
    }

    private void Awake()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
        _currentHealth = _characterDataManager.Data.MaxHealth;
    }
}