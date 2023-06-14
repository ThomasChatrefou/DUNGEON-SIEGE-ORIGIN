using NaughtyAttributes;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
    [Expandable]
    [SerializeField] private CharacterDataSO _data;
    public CharacterDataSO Data { get { return _data; } }

    public void ChangeWeapon(WeaponDataSO inWeapon)
    {
        _data.Weapon = inWeapon;
    }

    public void ChangeCharacter(CharacterDataSO inCharacter)
    {
        _data = inCharacter;
    }
}
