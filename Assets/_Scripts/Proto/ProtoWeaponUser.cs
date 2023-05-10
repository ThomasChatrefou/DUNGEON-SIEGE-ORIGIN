using System.Collections;
using UnityEngine;

// [TODO] move this in another script
public interface IWeaponUser
{
    public void StartWeaponUse();
    public void StopWeaponUse();
}

public class ProtoWeaponUser : MonoBehaviour, IWeaponUser
{
    // Variable that should belong to a SO
    [SerializeField] private float attackSpeed = 1.0f;

    [SerializeField] private Transform weaponTransform;
    private IWeapon weapon;

    private Coroutine runningCoroutine = null;

    private void Awake()
    {
        weapon = weaponTransform.GetComponent<IWeapon>();
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

    public IEnumerator HandleUseWeapon()
    {
        while (true)
        {
            // 
            weapon.Use();
            yield return new WaitForSeconds(1.0f / attackSpeed);
        }
    }
}
