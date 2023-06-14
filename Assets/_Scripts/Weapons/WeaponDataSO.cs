using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public int Damages;
    public float AttackSpeed;
    public float Range;
    [Expandable]
    public BaseAbilitySO Ability;
    public GameObject AbilityVisualEffectPrefab;
}
