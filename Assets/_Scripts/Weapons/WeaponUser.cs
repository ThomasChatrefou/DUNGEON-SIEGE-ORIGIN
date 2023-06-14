using System.Collections;
using UnityEngine;

public class WeaponUser : MonoBehaviour, IWeaponUser
{
    private CharacterDataManager _characterDataManager;
    private GameObject _weaponVisuals;
    private IAbilityVisualEffect _weaponAbilityVisualEffect;
    private Coroutine _runningCoroutine = null;
    private float _damages;
    private float _attackSpeed;
    private float _range;
    private IAbility _ability;

    public void StartWeaponUse()
    {
        if (_runningCoroutine != null) return;
        _runningCoroutine = StartCoroutine(HandleUseWeapon());
    }

    public void StopWeaponUse()
    {
        if (_runningCoroutine == null) return;
        StopCoroutine(_runningCoroutine);
        _runningCoroutine = null;
    }

    private void Awake()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
        CharacterDataSO data = _characterDataManager.Data;
        _damages = data.Weapon.Damages + data.BaseDamages;
        _attackSpeed = data.Weapon.AttackSpeed + data.BaseAttackSpeed;
        _range = data.Weapon.Range + data.BaseRange;
        _ability = data.Weapon.Ability;
    }

    private void Start()
    {
        WeaponDataSO weapon = _characterDataManager.Data.Weapon;
        if (weapon.AbilityVisualEffectPrefab != null)
        {
            _weaponVisuals = Instantiate(weapon.AbilityVisualEffectPrefab, transform);
            _weaponAbilityVisualEffect = _weaponVisuals.GetComponentInChildren<IAbilityVisualEffect>();
            IRescaler weaponEffectRescaler = _weaponVisuals.GetComponentInChildren<IRescaler>();
            weaponEffectRescaler?.Rescale(weapon.Range);
        }
    }

    private void OnDisable()
    {
        if (_runningCoroutine == null) return;
        StopCoroutine(_runningCoroutine);
    }

    private IEnumerator HandleUseWeapon()
    {
        if (Mathf.Abs(_attackSpeed) < Mathf.Epsilon) yield break;

        while (true)
        {
            AbilityBlackboard abilitydata = new()
            {
                Caster = transform,
                Targets = new(),
                VisualEffect = _weaponAbilityVisualEffect,
                Damages = _damages,
                Range = _range
            };
            _ability.Use(ref abilitydata);
            yield return new WaitForSeconds(1.0f / _attackSpeed);
        }
    }
}
