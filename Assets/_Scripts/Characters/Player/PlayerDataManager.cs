using UnityEngine;

public class PlayerDataManager : CharacterDataManager
{
    [SerializeField] private GameConfigSO _gameConfig;
    [SerializeField] private PlayerDataSO _playerData;

    // There should be something more elegant to find...
    public new CharacterDataSO Data { get { return Character; } }

    public CharacterDataSO Character
    {
        get
        {
            //Debug.Log("Character ID: " + DataPersistenceManager.instance.GetGameData().characterID + " weapon ID: " + DataPersistenceManager.instance.GetGameData().weaponID);
            if (_gameConfig.TryGetCharacter(DataPersistenceManager.instance.GetGameData().characterID /*_playerData.CurrentCharacterId*/, out CharacterDataSO character))
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
            if (_gameConfig.TryGetWeapon(DataPersistenceManager.instance.GetGameData().weaponID /*_playerData.CurrentWeaponId*/, out WeaponDataSO weapon))
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
                    int nbCurrentUpgradeToApply = DataPersistenceManager.instance.GetGameData().weaponUpgrade[_gameConfig.GetId(upgrade)]; //_playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
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
                    int nbCurrentUpgradeToApply = DataPersistenceManager.instance.GetGameData().weaponUpgrade[_gameConfig.GetId(upgrade)]; // _playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
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
                    int nbCurrentUpgradeToApply = DataPersistenceManager.instance.GetGameData().weaponUpgrade[_gameConfig.GetId(upgrade)]; //_playerData.CountByUpgradeId[_gameConfig.GetId(upgrade)];
                    result += upgrade.Range * nbCurrentUpgradeToApply;
                }
            }
            return result;
        }
    }
}
