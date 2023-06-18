using UnityEngine;

public class PlayerDataManager : CharacterDataManager, IDataPersistence
{
    [SerializeField] private GameConfigSO _gameConfig;
    [SerializeField] private PlayerDataSO _playerData;

    // There should be something more elegant to find...
    public new CharacterDataSO Data { get { return Character; } }

    public CharacterDataSO Character
    {
        get
        {
            if (_gameConfig.TryGetCharacter(_playerData.CurrentCharacterId, out CharacterDataSO character))
            {
                return character;
            }
            _playerData.CurrentCharacterId = _gameConfig.GetId(_characterData);
            return _characterData;
        }
        set
        {
            _playerData.CurrentCharacterId = _gameConfig.GetId(value);
        }
    }

    public WeaponDataSO Weapon
    {
        get
        {
            CharacterDataSO character = Character;
            if (_gameConfig.TryGetWeapon(_playerData.CurrentWeaponId, out WeaponDataSO weapon))
            {
                return weapon;
            }
            _playerData.CurrentWeaponId = _gameConfig.GetId(character.BaseWeapon);
            return character.BaseWeapon;
        }
        set
        {
            _playerData.CurrentWeaponId = _gameConfig.GetId(value);
        }
    }

    public float Damages
    {
        get
        {
            WeaponDataSO weapon = Weapon;
            float result = Character.BaseDamages + weapon.Damages;
            if (_gameConfig.TryGetWeaponUpgrades(weapon, out WeaponStatisticUgradeSO[] upgrades))
            {
                foreach (WeaponStatisticUgradeSO upgrade in upgrades)
                {
                    int nbCurrentUpgradeToApply = _playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
                    result += upgrade.Damages * nbCurrentUpgradeToApply;
                }
            }
            return result;
        }
    }

    public float AttackSpeed
    {
        get
        {
            WeaponDataSO weapon = Weapon;
            float result = Character.BaseAttackSpeed + weapon.AttackSpeed;
            if (_gameConfig.TryGetWeaponUpgrades(weapon, out WeaponStatisticUgradeSO[] upgrades))
            {
                foreach (WeaponStatisticUgradeSO upgrade in upgrades)
                {
                    int nbCurrentUpgradeToApply = _playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
                    result += upgrade.AttackSpeed * nbCurrentUpgradeToApply;
                }
            }
            return result;
        }
    }

    public float Range
    {
        get
        {
            WeaponDataSO weapon = Weapon;
            float result = Character.BaseRange + weapon.Range;
            if (_gameConfig.TryGetWeaponUpgrades(weapon, out WeaponStatisticUgradeSO[] upgrades))
            {
                foreach (WeaponStatisticUgradeSO upgrade in upgrades)
                {
                    int nbCurrentUpgradeToApply = _playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
                    result += upgrade.Range * nbCurrentUpgradeToApply;
                }
            }
            return result;
        }
    }

    public void LoadData(GameData data)
    {
        _playerData.CurrentCharacterId = data.weaponID;
        _playerData.CurrentWeaponId = data.weaponID;
    }

    public void SaveData(ref GameData data)
    {
        data.weaponID = _playerData.CurrentWeaponId;
    }
}
