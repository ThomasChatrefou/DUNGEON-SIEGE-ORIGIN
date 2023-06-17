using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatisticUgrade", menuName = "ScriptableObjects/Weapons/WeaponStatisticUgrade")]
public class WeaponStatisticUgradeSO : ScriptableObject
{
    public int Damages;
    public float AttackSpeed;
    public float Range;
}
