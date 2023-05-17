using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponUpgrade : MonoBehaviour, IWeapon
{
    [SerializeField] protected IWeapon weaponChild;

    public abstract void Use();
    public abstract List<Vector3> GetLastHitPositions();
}

public class WeaponUpgradeSnowballEffect : WeaponUpgrade
{
    private List<Vector3> lastHitPositions;

    public override void Use()
    {
        weaponChild.Use();

    }

    public override List<Vector3> GetLastHitPositions()
    {
        return lastHitPositions;
    }

}

public interface Snowballable
{
    public abstract List<Vector3> GetLastHitPositions();

}
