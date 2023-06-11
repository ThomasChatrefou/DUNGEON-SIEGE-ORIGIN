using System.Collections;
using UnityEngine;

public class WeaponUser : MonoBehaviour, IWeaponUser
{
    [SerializeField] private CharacterDataSO _characterData;
    [SerializeField] private WeaponDataSO _weaponData;

    private Coroutine runningCoroutine = null;
    private GameObject _weaponVisuals;
    private IAbilityVisualEffect _weaponAbilityVisualEffect;

    private float _damages;
    private float _attackSpeed;

    private void Start()
    {
        if (_weaponData.AbilityVisualEffectPrefab != null)
        {
            _weaponVisuals = Instantiate(_weaponData.AbilityVisualEffectPrefab, transform);
        
            _weaponAbilityVisualEffect = _weaponVisuals.GetComponentInChildren<IAbilityVisualEffect>();

            IRescaler weaponEffectRescaler = _weaponVisuals.GetComponentInChildren<IRescaler>();
            weaponEffectRescaler?.Rescale(_weaponData.Range);
        }
        
        _damages = _weaponData.Damages + _characterData.Damages;
        _attackSpeed = _weaponData.AttackSpeed + _characterData.AttackSpeed;
    }

    public void StartWeaponUse()
    {
        if (runningCoroutine == null)
        {
            runningCoroutine = StartCoroutine(HandleUseWeapon());
        }
    }

    public void StopWeaponUse()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }

    private IEnumerator HandleUseWeapon()
    {
        if (Mathf.Abs(_attackSpeed) < Mathf.Epsilon)
        {
            yield break;
        }

        while (true)
        {
            AbilityBlackboard abilitydata = new()
            {
                Caster = transform,
                Targets = new(),
                VisualEffect = _weaponAbilityVisualEffect,
                Damages = _damages,
                Range = _weaponData.Range
            };
            _weaponData.Ability.Use(ref abilitydata);
            yield return new WaitForSeconds(1.0f / _attackSpeed);
        }
    }

    private void OnDisable()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
    }
}
