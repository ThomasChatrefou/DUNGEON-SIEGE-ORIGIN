using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/Character")]
public class CharacterDataSO : ScriptableObject
{
    public float MaxHealth;
    public float MovementSpeed;
    public float BaseDamages;
    public float BaseAttackSpeed;
    public float BaseRange;
    [Expandable]
    public WeaponDataSO Weapon;
    public GameObject CharacterLook;
}
